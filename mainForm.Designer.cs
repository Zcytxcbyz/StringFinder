namespace StringFinder
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.Dirty_textBox = new System.Windows.Forms.TextBox();
            this.Directory_Label = new System.Windows.Forms.Label();
            this.Open_Btn = new System.Windows.Forms.Button();
            this.Search_Label = new System.Windows.Forms.Label();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.listView_result = new System.Windows.Forms.ListView();
            this.ch_item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cb_encoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_QuickSearch = new System.Windows.Forms.CheckBox();
            this.IgnoreCase = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Dirty_textBox
            // 
            resources.ApplyResources(this.Dirty_textBox, "Dirty_textBox");
            this.Dirty_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dirty_textBox.Name = "Dirty_textBox";
            // 
            // Directory_Label
            // 
            resources.ApplyResources(this.Directory_Label, "Directory_Label");
            this.Directory_Label.Name = "Directory_Label";
            // 
            // Open_Btn
            // 
            resources.ApplyResources(this.Open_Btn, "Open_Btn");
            this.Open_Btn.Name = "Open_Btn";
            this.Open_Btn.UseVisualStyleBackColor = true;
            this.Open_Btn.Click += new System.EventHandler(this.Open_Btn_Click);
            // 
            // Search_Label
            // 
            resources.ApplyResources(this.Search_Label, "Search_Label");
            this.Search_Label.Name = "Search_Label";
            // 
            // SearchBox
            // 
            resources.ApplyResources(this.SearchBox, "SearchBox");
            this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchBox.Name = "SearchBox";
            // 
            // SearchButton
            // 
            resources.ApplyResources(this.SearchButton, "SearchButton");
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // listView_result
            // 
            resources.ApplyResources(this.listView_result, "listView_result");
            this.listView_result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_result.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_item,
            this.ch_file,
            this.ch_index});
            this.listView_result.FullRowSelect = true;
            this.listView_result.HideSelection = false;
            this.listView_result.MultiSelect = false;
            this.listView_result.Name = "listView_result";
            this.listView_result.UseCompatibleStateImageBehavior = false;
            this.listView_result.View = System.Windows.Forms.View.Details;
            this.listView_result.DoubleClick += new System.EventHandler(this.listView_result_DoubleClick);
            this.listView_result.MouseLeave += new System.EventHandler(this.listView_result_MouseLeave);
            this.listView_result.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_result_MouseMove);
            this.listView_result.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_result_MouseUp);
            // 
            // ch_item
            // 
            resources.ApplyResources(this.ch_item, "ch_item");
            // 
            // ch_file
            // 
            resources.ApplyResources(this.ch_file, "ch_file");
            // 
            // ch_index
            // 
            resources.ApplyResources(this.ch_index, "ch_index");
            // 
            // cb_encoding
            // 
            this.cb_encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_encoding.FormattingEnabled = true;
            this.cb_encoding.Items.AddRange(new object[] {
            resources.GetString("cb_encoding.Items"),
            resources.GetString("cb_encoding.Items1"),
            resources.GetString("cb_encoding.Items2"),
            resources.GetString("cb_encoding.Items3"),
            resources.GetString("cb_encoding.Items4"),
            resources.GetString("cb_encoding.Items5")});
            resources.ApplyResources(this.cb_encoding, "cb_encoding");
            this.cb_encoding.Name = "cb_encoding";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cb_QuickSearch
            // 
            resources.ApplyResources(this.cb_QuickSearch, "cb_QuickSearch");
            this.cb_QuickSearch.Checked = global::StringFinder.Properties.Settings.Default.QuickSearch;
            this.cb_QuickSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_QuickSearch.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::StringFinder.Properties.Settings.Default, "QuickSearch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cb_QuickSearch.Name = "cb_QuickSearch";
            this.cb_QuickSearch.UseVisualStyleBackColor = true;
            // 
            // IgnoreCase
            // 
            resources.ApplyResources(this.IgnoreCase, "IgnoreCase");
            this.IgnoreCase.Checked = global::StringFinder.Properties.Settings.Default.IgnoreCase;
            this.IgnoreCase.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::StringFinder.Properties.Settings.Default, "IgnoreCase", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IgnoreCase.Name = "IgnoreCase";
            this.IgnoreCase.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_QuickSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_encoding);
            this.Controls.Add(this.listView_result);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.IgnoreCase);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.Search_Label);
            this.Controls.Add(this.Open_Btn);
            this.Controls.Add(this.Directory_Label);
            this.Controls.Add(this.Dirty_textBox);
            this.Name = "mainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Dirty_textBox;
        private System.Windows.Forms.Label Directory_Label;
        private System.Windows.Forms.Button Open_Btn;
        private System.Windows.Forms.Label Search_Label;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.CheckBox IgnoreCase;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ListView listView_result;
        private System.Windows.Forms.ColumnHeader ch_item;
        private System.Windows.Forms.ColumnHeader ch_file;
        private System.Windows.Forms.ComboBox cb_encoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader ch_index;
        private System.Windows.Forms.CheckBox cb_QuickSearch;
    }
}

