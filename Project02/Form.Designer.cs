namespace Project02
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RadiusBox = new System.Windows.Forms.NumericUpDown();
            this.HeightBox = new System.Windows.Forms.NumericUpDown();
            this.WidthBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.MarginBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ThicknessBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThicknessBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "x";
            // 
            // RadiusBox
            // 
            this.RadiusBox.Location = new System.Drawing.Point(298, 12);
            this.RadiusBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.RadiusBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.RadiusBox.Name = "RadiusBox";
            this.RadiusBox.Size = new System.Drawing.Size(120, 19);
            this.RadiusBox.TabIndex = 3;
            this.RadiusBox.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // HeightBox
            // 
            this.HeightBox.Location = new System.Drawing.Point(155, 12);
            this.HeightBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.HeightBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(120, 19);
            this.HeightBox.TabIndex = 4;
            this.HeightBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(12, 12);
            this.WidthBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WidthBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(120, 19);
            this.WidthBox.TabIndex = 5;
            this.WidthBox.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "+";
            // 
            // MarginBox
            // 
            this.MarginBox.Location = new System.Drawing.Point(441, 12);
            this.MarginBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MarginBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MarginBox.Name = "MarginBox";
            this.MarginBox.Size = new System.Drawing.Size(120, 19);
            this.MarginBox.TabIndex = 3;
            this.MarginBox.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(567, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "|";
            // 
            // ThicknessBox
            // 
            this.ThicknessBox.Location = new System.Drawing.Point(581, 12);
            this.ThicknessBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ThicknessBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ThicknessBox.Name = "ThicknessBox";
            this.ThicknessBox.Size = new System.Drawing.Size(120, 19);
            this.ThicknessBox.TabIndex = 3;
            this.ThicknessBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ThicknessBox);
            this.Controls.Add(this.MarginBox);
            this.Controls.Add(this.RadiusBox);
            this.Controls.Add(this.HeightBox);
            this.Controls.Add(this.WidthBox);
            this.Name = "Form";
            this.Text = "XLSX";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.RadiusBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThicknessBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown RadiusBox;
        private System.Windows.Forms.NumericUpDown HeightBox;
        private System.Windows.Forms.NumericUpDown WidthBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MarginBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown ThicknessBox;
    }
}

