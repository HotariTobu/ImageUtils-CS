namespace PNG最適化
{
    partial class Form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.list = new System.Windows.Forms.ListBox();
            this.outputPathBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.process = new System.Diagnostics.Process();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.AllowDrop = true;
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.FormattingEnabled = true;
            this.list.IntegralHeight = false;
            this.list.ItemHeight = 12;
            this.list.Location = new System.Drawing.Point(12, 12);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(776, 397);
            this.list.TabIndex = 0;
            this.list.DragDrop += new System.Windows.Forms.DragEventHandler(this.list_DragDrop);
            this.list.DragEnter += new System.Windows.Forms.DragEventHandler(this.list_DragEnter);
            // 
            // outputPathBox
            // 
            this.outputPathBox.AllowDrop = true;
            this.outputPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPathBox.Location = new System.Drawing.Point(47, 417);
            this.outputPathBox.Name = "outputPathBox";
            this.outputPathBox.Size = new System.Drawing.Size(660, 19);
            this.outputPathBox.TabIndex = 1;
            this.outputPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.outputPathBox_DragDrop);
            this.outputPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.outputPathBox_DragEnter);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(713, 415);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "出力";
            // 
            // process
            // 
            this.process.StartInfo.Domain = "";
            this.process.StartInfo.FileName = "pngcrush.exe";
            this.process.StartInfo.LoadUserProfile = false;
            this.process.StartInfo.Password = null;
            this.process.StartInfo.StandardErrorEncoding = null;
            this.process.StartInfo.StandardOutputEncoding = null;
            this.process.StartInfo.UserName = "";
            this.process.SynchronizingObject = this;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.outputPathBox);
            this.Controls.Add(this.list);
            this.Name = "Form";
            this.Text = "PNG最適化";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.TextBox outputPathBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Diagnostics.Process process;
    }
}

