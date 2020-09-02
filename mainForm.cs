using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Timers;

namespace StringFinder
{
    public partial class mainForm : Form
    {
        private List<Task> taskList = new List<Task>();
        private ToolTip mainToolTip = new ToolTip();
        private ListViewItem.ListViewSubItem PrevListViewItem = null;
        private double chItemWidthPro, chFileWidthPro, chIndexWidthPro;
        private delegate void AddData(ListViewItem lvi);
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
            chItemWidthPro = 0.2;
            chFileWidthPro = 0.6;
            chIndexWidthPro = 0.2;
            listView_result_SizeChanged(null, null);
            cb_encoding.SelectedIndex = 0;
        }

        private void Open_Btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择文件夹";
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
                MessageBox.Show("未能找到指定路径。", this.Text, MessageBoxButtons.OK);
                return;
            }
            if (!cb_QuickSearch.Checked)
            {
                listView_result.Items.Clear();
                Task.Factory.StartNew(StringFinderDeeply, path);
                return;
            }
            Task task = new Task(StringFinderRegex, path);
            task.Start();
            taskList.Add(task);
            Task.Factory.StartNew(() =>
            {
                while (taskList.Exists(t => !t.IsCompleted))
                {
                    Task.WaitAll(taskList.ToArray());
                }
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
                    ToolStripMenuItem copyItem = new ToolStripMenuItem("复制");
                    copyItem.Click += new EventHandler((_, _) =>
                    {
                        Clipboard.SetText(row.SubItems[1].Text);
                        
                    });
                    ToolStripMenuItem openItem = new ToolStripMenuItem("打开");
                    openItem.Click += new EventHandler((_, _) =>
                    {
                        System.Diagnostics.Process.Start(row.SubItems[1].Text);
                    });
                    menuStrip.Items.AddRange(new ToolStripItem[] { copyItem, openItem });
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
                    mainToolTip.Show(cell.Text, (Control)sender, e.X + 20, e.Y, 2000);
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
