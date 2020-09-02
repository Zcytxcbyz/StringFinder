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
        private class DataItem
        {
            public string Item, File, Index;
            public DataItem(string Item, string File, string Index)
            {
                this.Item = Item;
                this.File = File;
                this.Index = Index;
            }
        }
        private List<Task> taskList = new List<Task>();
        private List<DataItem> dataItems = new List<DataItem>();
        private ToolTip mainToolTip = new ToolTip();
        private ListViewItem.ListViewSubItem PrevListViewItem = null;
        private double chItemWidthPro, chFileWidthPro, chIndexWidthPro;
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
            dataItems.Clear();
            Task task = new Task(StringFinderRegex, path);
            task.Start();
            taskList.Add(task);
            FrmProgressBar frmProgressBar = new FrmProgressBar();
            Task updateTask = Task.WhenAll(taskList).ContinueWith((_) =>
              {
                  Invoke(new Action(() => { listView_result.Items.Clear(); }));
                  foreach (DataItem item in dataItems)
                  {
                      if (item == null) continue;
                      ListViewItem lvi = new ListViewItem(item.Item);
                      lvi.SubItems.Add(item.File);
                      lvi.SubItems.Add(item.Index);
                      Invoke(new Action(() => { listView_result.Items.Add(lvi); }));
                  }
                  if (frmProgressBar.Visible) Invoke(new Action(() => { frmProgressBar.Close(); }));
              });       
            System.Timers.Timer timer = new System.Timers.Timer(2000) { AutoReset = false };
            timer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) => 
            {
                if (!updateTask.IsCompleted)
                {
                    Invoke(new Action(() =>{frmProgressBar.ShowDialog(this);}));
                }
            });
            timer.Start();
        }
        private void StringFinderRegex(object path)
        {
            try
            {
                string[] fileList = Directory.GetFiles((string)path);
                string[] folderList = Directory.GetDirectories((string)path);
                Task.Factory.StartNew(() =>
                {
                    foreach (string folder in folderList)
                    {
                        Task task = new Task(StringFinderRegex, folder);
                        task.Start();
                        taskList.Add(task);
                    }
                }, TaskCreationOptions.AttachedToParent);
                foreach (string file in fileList)
                {
                    Task.Factory.StartNew(() =>
                    {
                        FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs, FinderParams.encoding);
                        string content = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                        MatchCollection mc = Regex.Matches(content, FinderParams.searchText, FinderParams.regexOptions);
                        foreach (Match m in mc)
                        {
                            dataItems.Add(new DataItem(m.Value, file, m.Index.ToString()));
                        }
                    }, TaskCreationOptions.AttachedToParent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK);
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
