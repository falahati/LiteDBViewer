namespace LiteDBViewer
{
    partial class BinaryViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rb_Image = new System.Windows.Forms.RadioButton();
            this.rb_UTF = new System.Windows.Forms.RadioButton();
            this.rb_Ascii = new System.Windows.Forms.RadioButton();
            this.rb_Hex = new System.Windows.Forms.RadioButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinaryViewForm));
            this.textBox = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rb_Image
            // 
            this.rb_Image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_Image.AutoSize = true;
            this.rb_Image.Location = new System.Drawing.Point(600, 81);
            this.rb_Image.Name = "rb_Image";
            this.rb_Image.Size = new System.Drawing.Size(94, 17);
            this.rb_Image.TabIndex = 11;
            this.rb_Image.Text = "View as Image";
            this.rb_Image.UseVisualStyleBackColor = true;
            this.rb_Image.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_UTF
            // 
            this.rb_UTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_UTF.AutoSize = true;
            this.rb_UTF.Location = new System.Drawing.Point(600, 58);
            this.rb_UTF.Name = "rb_UTF";
            this.rb_UTF.Size = new System.Drawing.Size(92, 17);
            this.rb_UTF.TabIndex = 10;
            this.rb_UTF.Text = "View as UTF8";
            this.rb_UTF.UseVisualStyleBackColor = true;
            this.rb_UTF.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_Ascii
            // 
            this.rb_Ascii.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_Ascii.AutoSize = true;
            this.rb_Ascii.Location = new System.Drawing.Point(600, 35);
            this.rb_Ascii.Name = "rb_Ascii";
            this.rb_Ascii.Size = new System.Drawing.Size(87, 17);
            this.rb_Ascii.TabIndex = 9;
            this.rb_Ascii.Text = "View as Ascii";
            this.rb_Ascii.UseVisualStyleBackColor = true;
            this.rb_Ascii.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_Hex
            // 
            this.rb_Hex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_Hex.AutoSize = true;
            this.rb_Hex.Checked = true;
            this.rb_Hex.Location = new System.Drawing.Point(600, 12);
            this.rb_Hex.Name = "rb_Hex";
            this.rb_Hex.Size = new System.Drawing.Size(84, 17);
            this.rb_Hex.TabIndex = 8;
            this.rb_Hex.TabStop = true;
            this.rb_Hex.Text = "View as Hex";
            this.rb_Hex.UseVisualStyleBackColor = true;
            this.rb_Hex.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.SystemColors.Window;
            this.textBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBox.Location = new System.Drawing.Point(12, 10);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(572, 439);
            this.textBox.TabIndex = 6;
            this.textBox.WordWrap = false;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 10);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(572, 439);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // BinaryViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.rb_Image);
            this.Controls.Add(this.rb_UTF);
            this.Controls.Add(this.rb_Ascii);
            this.Controls.Add(this.rb_Hex);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 250);
            this.Name = "BinaryViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Binary Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_Image;
        private System.Windows.Forms.RadioButton rb_UTF;
        private System.Windows.Forms.RadioButton rb_Ascii;
        private System.Windows.Forms.RadioButton rb_Hex;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}