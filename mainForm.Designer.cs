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
            this.Dirty_textBox = new System.Windows.Forms.TextBox();
            this.Directory_Label = new System.Windows.Forms.Label();
            this.Open_Btn = new System.Windows.Forms.Button();
            this.Search_Label = new System.Windows.Forms.Label();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.IgnoreCase = new System.Windows.Forms.CheckBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.listView_result = new System.Windows.Forms.ListView();
            this.ch_item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cb_encoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Dirty_textBox
            // 
            this.Dirty_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dirty_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dirty_textBox.Location = new System.Drawing.Point(53, 12);
            this.Dirty_textBox.Name = "Dirty_textBox";
            this.Dirty_textBox.Size = new System.Drawing.Size(635, 21);
            this.Dirty_textBox.TabIndex = 0;
            // 
            // Directory_Label
            // 
            this.Directory_Label.AutoSize = true;
            this.Directory_Label.Location = new System.Drawing.Point(18, 14);
            this.Directory_Label.Name = "Directory_Label";
            this.Directory_Label.Size = new System.Drawing.Size(29, 12);
            this.Directory_Label.TabIndex = 1;
            this.Directory_Label.Text = "目录";
            // 
            // Open_Btn
            // 
            this.Open_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Open_Btn.Location = new System.Drawing.Point(697, 12);
            this.Open_Btn.Name = "Open_Btn";
            this.Open_Btn.Size = new System.Drawing.Size(75, 23);
            this.Open_Btn.TabIndex = 2;
            this.Open_Btn.Text = "打开目录";
            this.Open_Btn.UseVisualStyleBackColor = true;
            this.Open_Btn.Click += new System.EventHandler(this.Open_Btn_Click);
            // 
            // Search_Label
            // 
            this.Search_Label.AutoSize = true;
            this.Search_Label.Location = new System.Drawing.Point(18, 43);
            this.Search_Label.Name = "Search_Label";
            this.Search_Label.Size = new System.Drawing.Size(29, 12);
            this.Search_Label.TabIndex = 3;
            this.Search_Label.Text = "搜索";
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchBox.Location = new System.Drawing.Point(53, 41);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(635, 21);
            this.SearchBox.TabIndex = 4;
            // 
            // IgnoreCase
            // 
            this.IgnoreCase.AutoSize = true;
            this.IgnoreCase.Location = new System.Drawing.Point(53, 69);
            this.IgnoreCase.Name = "IgnoreCase";
            this.IgnoreCase.Size = new System.Drawing.Size(84, 16);
            this.IgnoreCase.TabIndex = 6;
            this.IgnoreCase.Text = "忽略大小写";
            this.IgnoreCase.UseVisualStyleBackColor = true;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(697, 41);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 7;
            this.SearchButton.Text = "查找";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // listView_result
            // 
            this.listView_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_result.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_item,
            this.ch_file,
            this.ch_index});
            this.listView_result.FullRowSelect = true;
            this.listView_result.HideSelection = false;
            this.listView_result.Location = new System.Drawing.Point(12, 93);
            this.listView_result.MultiSelect = false;
            this.listView_result.Name = "listView_result";
            this.listView_result.Size = new System.Drawing.Size(760, 456);
            this.listView_result.TabIndex = 8;
            this.listView_result.UseCompatibleStateImageBehavior = false;
            this.listView_result.View = System.Windows.Forms.View.Details;
            this.listView_result.DoubleClick += new System.EventHandler(this.listView_result_DoubleClick);
            this.listView_result.MouseLeave += new System.EventHandler(this.listView_result_MouseLeave);
            this.listView_result.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_result_MouseMove);
            this.listView_result.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_result_MouseUp);
            // 
            // ch_item
            // 
            this.ch_item.Text = "匹配项";
            this.ch_item.Width = 25;
            // 
            // ch_file
            // 
            this.ch_file.Text = "文件";
            this.ch_file.Width = 25;
            // 
            // ch_index
            // 
            this.ch_index.Text = "索引";
            this.ch_index.Width = 25;
            // 
            // cb_encoding
            // 
            this.cb_encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_encoding.FormattingEnabled = true;
            this.cb_encoding.Items.AddRange(new object[] {
            "Default",
            "UTF8",
            "ASCII",
            "Unicode",
            "UTF7",
            "UTF32"});
            this.cb_encoding.Location = new System.Drawing.Point(178, 67);
            this.cb_encoding.Name = "cb_encoding";
            this.cb_encoding.Size = new System.Drawing.Size(74, 20);
            this.cb_encoding.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "编码";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
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
            this.Text = "字符串查找器";
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
    }
}

