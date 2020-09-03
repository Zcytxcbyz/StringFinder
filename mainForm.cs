using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringFinder
{
    public partial class mainForm : Form
    {
        private List<Task> taskList = new List<Task>();
        private ToolTip mainToolTip = new ToolTip();
        private ListViewItem.ListViewSubItem PrevListViewItem = null;
        private double chItemWidthPro, chFileWidthPro, chIndexWidthPro;
        private delegate void AddData(ListViewItem lvi);
        private ComponentResourceManager resources = new ComponentResourceManager(typeof(Properties.Resources));
        private static class FinderParams
        {
            public static Encoding encoding;
            public static string path, searchText;
            public static RegexOptions regexOptions;
        }
        public mainForm()
        {
            InitializeComponent();
        }
        private delegate void addData(ListViewItem item);
        private void mainForm_Load(object sender, EventArgs e)
        {
            chItemWidthPro = Properties.Settings.Default.chItemWidthPro;
            chFileWidthPro = Properties.Settings.Default.chFileWidthPro;
            chIndexWidthPro = Properties.Settings.Default.chIndexWidthPro;
            listView_result_SizeChanged(null, null);
            cb_encoding.SelectedIndex = Properties.Settings.Default.Encoding;
            this.Size = Properties.Settings.Default.FormSize;
            if (Properties.Settings.Default.FormLocation != null)
                this.Location = Properties.Settings.Default.FormLocation;
            else
                Properties.Settings.Default.FormLocation = this.Location;
            this.WindowState = Properties.Settings.Default.FormState;
            this.LocationChanged += new EventHandler(mainForm_LocationChanged);
            this.SizeChanged += new EventHandler(mainForm_SizeChanged);
        }

        private void Open_Btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = resources.GetString("FolderBrowserDialog.Description");
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Dirty_textBox.Text = fbd.SelectedPath;
            }
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            string path = Dirty_textBox.Text;
            switch (cb_encoding.Text)
            {
                case "Default": FinderParams.encoding = Encoding.Default; break;
                case "UTF8": FinderParams.encoding = Encoding.UTF8; break;
                case "ASCII": FinderParams.encoding = Encoding.ASCII; break;
                case "Unicode": FinderParams.encoding = Encoding.Unicode; break;
                case "UTF7": FinderParams.encoding = Encoding.UTF7; break;
                case "UTF32": FinderParams.encoding = Encoding.UTF32; break;
            }
            FinderParams.path = path;
            FinderParams.regexOptions = IgnoreCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None;
            FinderParams.searchText = SearchBox.Text;
            taskList.Clear();
            listView_result.Items.Clear();
            if (!Directory.Exists(path))
            {
                MessageBox.Show(resources.GetString("PathNotFoundException.Message"), this.Text, MessageBoxButtons.OK);
                return;
            }
            if (!cb_QuickSearch.Checked)
            {
                listView_result.Items.Clear();
                Task.Factory.StartNew(StringFinderDeeply, path).ContinueWith((_) => 
                {
                    taskList.Clear();
                });
            }
            else
            {
                Task task = new Task(StringFinderRegex, path);
                task.Start();
                taskList.Add(task);
                Task.Factory.StartNew(() =>
                {
                    while (taskList.Exists(t => !t.IsCompleted))
                    {
                        Task.WaitAll(taskList.ToArray());
                    }
                }).ContinueWith((_) =>
                {
                    taskList.Clear();
                });
                Task.Factory.StartNew(() =>
                {
                    while (taskList.Exists(t => !t.IsCompleted))
                    {
                        taskList.RemoveAll(t => t.IsCompleted);
                        Thread.Sleep(2000);
                    }
                });
            }
        }

        private void AddDataToView(ListViewItem lvi)
        {
            listView_result.Items.Add(lvi);
        }

        private void StringFinderDeeply(object path)
        {
            string[] fileList = Directory.GetFiles((string)path);
            string[] folderList = Directory.GetDirectories((string)path);
            foreach (string file in fileList)
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, FinderParams.encoding);
                string content = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                MatchCollection mc = Regex.Matches(content, FinderParams.searchText, FinderParams.regexOptions);
                foreach (Match m in mc)
                {
                    ListViewItem lvi = new ListViewItem(m.Value);
                    lvi.SubItems.Add(file);
                    lvi.SubItems.Add(m.Index.ToString());
                    Invoke(new AddData(AddDataToView), lvi);
                }
            }
            foreach (string folder in folderList)
            {
                StringFinderDeeply(folder);
            }
        }
        private void StringFinderRegex(object path)
        {
            string[] fileList = Directory.GetFiles((string)path);
            string[] folderList = Directory.GetDirectories((string)path);
            taskList.Add(Task.Factory.StartNew(() =>
            {
                foreach (string folder in folderList)
                {
                    Task task = new Task(StringFinderRegex, folder);
                    task.Start();
                    taskList.Add(task);      
                }
            }, TaskCreationOptions.AttachedToParent));
            foreach (string file in fileList)
            {
                taskList.Add(Task.Factory.StartNew(() =>
                {
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, FinderParams.encoding);
                    string content = sr.ReadToEnd();
                    sr.Close();
                    fs.Close();
                    MatchCollection mc = Regex.Matches(content, FinderParams.searchText, FinderParams.regexOptions);
                    foreach (Match m in mc)
                    {
                        ListViewItem lvi = new ListViewItem(m.Value);
                        lvi.SubItems.Add(file);
                        lvi.SubItems.Add(m.Index.ToString());
                        BeginInvoke(new AddData(AddDataToView), lvi);
                    }
                }, TaskCreationOptions.AttachedToParent));
            }
        }

        private void listView_result_SizeChanged(object sender, EventArgs e)
        {
            listView_result.SizeChanged -= new EventHandler(listView_result_SizeChanged);
            listView_result.ColumnWidthChanged -= new ColumnWidthChangedEventHandler(listView_result_ColumnWidthChanged);
            ch_item.Width = (int)(listView_result.Width*chItemWidthPro);
            ch_file.Width = (int)(listView_result.Width*chFileWidthPro);
            ch_index.Width = (int)(listView_result.Width*chIndexWidthPro);
            listView_result.ColumnWidthChanged += new ColumnWidthChangedEventHandler(listView_result_ColumnWidthChanged);
            listView_result.SizeChanged += new EventHandler(listView_result_SizeChanged);
        }

        private void listView_result_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem row = ((ListView)sender).GetItemAt(e.X, e.Y);
                if (row != null && row.Selected)
                {
                    ContextMenuStrip menuStrip = new ContextMenuStrip();
                    ToolStripMenuItem copyItem = new ToolStripMenuItem(resources.GetString("CopyMenuItem.Text"));
                    copyItem.Click += new EventHandler((_, _) =>
                    {
                        Clipboard.SetText(row.SubItems[1].Text);
                        
                    });
                    ToolStripMenuItem openItem = new ToolStripMenuItem(resources.GetString("OpenMenuItem.Text"));
                    openItem.Click += new EventHandler((_, _) =>
                    {
                        System.Diagnostics.Process.Start(row.SubItems[1].Text);
                    });
                    ToolStripMenuItem viewItem = new ToolStripMenuItem(resources.GetString("ViewMenuItem.Text"));
                    viewItem.Click += new EventHandler((_, _) =>
                    {
                        System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(row.SubItems[1].Text));
                    });
                    menuStrip.Items.AddRange(new ToolStripItem[] { copyItem, openItem, viewItem });
                    menuStrip.Show((Control)sender,e.Location);
                }
            }
        }
        private void listView_result_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem row = ((ListView)sender).GetItemAt(e.X, e.Y);
            if (row == null)
            {
                listView_result_MouseLeave(sender, e);
                return;
            }
            ListViewItem.ListViewSubItem cell = row.GetSubItemAt(e.X, e.Y);
            if (cell != null && cell != PrevListViewItem)
            {
                if (cell.Text != null)
                    mainToolTip.Show(cell.Text, (Control)sender, e.X, e.Y + 20, 10000);
                PrevListViewItem = cell;
            }
            else if(cell != PrevListViewItem)
            {
                listView_result_MouseLeave(sender, e);
            }
        }

        private void listView_result_MouseLeave(object sender, EventArgs e)
        {
            mainToolTip.Hide((Control)sender);
            PrevListViewItem = null;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormState = this.WindowState;
            Properties.Settings.Default.Encoding = cb_encoding.SelectedIndex;
            Properties.Settings.Default.chItemWidthPro = chItemWidthPro;
            Properties.Settings.Default.chFileWidthPro = chFileWidthPro;
            Properties.Settings.Default.chIndexWidthPro = chIndexWidthPro;
            Properties.Settings.Default.Save();
        }

        private void mainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.FormSize = this.Size;
            }
        }

        private void mainForm_LocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.FormLocation = this.Location;
            }
        }

        private void listView_result_DoubleClick(object sender, EventArgs e)
        {
            string Filename = listView_result.SelectedItems[0].SubItems[1].Text;
            System.Diagnostics.Process.Start(Filename);
        }

        private void listView_result_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            chItemWidthPro = (double)ch_item.Width / listView_result.Width;
            chFileWidthPro = (double)ch_file.Width / listView_result.Width;
            chIndexWidthPro = (double)ch_index.Width / listView_result.Width;
        }
    }
}
