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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinaryViewForm));
            this.rb_image = new System.Windows.Forms.RadioButton();
            this.rb_utf8 = new System.Windows.Forms.RadioButton();
            this.rb_ascii = new System.Windows.Forms.RadioButton();
            this.rb_hex = new System.Windows.Forms.RadioButton();
            this.textBox = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.rb_utf32 = new System.Windows.Forms.RadioButton();
            this.rb_utf7 = new System.Windows.Forms.RadioButton();
            this.rb_unicode = new System.Windows.Forms.RadioButton();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_asString = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rb_image
            // 
            this.rb_image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_image.AutoSize = true;
            this.rb_image.Location = new System.Drawing.Point(593, 150);
            this.rb_image.Name = "rb_image";
            this.rb_image.Size = new System.Drawing.Size(94, 17);
            this.rb_image.TabIndex = 7;
            this.rb_image.Text = "View as Image";
            this.rb_image.UseVisualStyleBackColor = true;
            this.rb_image.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_utf8
            // 
            this.rb_utf8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_utf8.AutoSize = true;
            this.rb_utf8.Location = new System.Drawing.Point(593, 79);
            this.rb_utf8.Name = "rb_utf8";
            this.rb_utf8.Size = new System.Drawing.Size(92, 17);
            this.rb_utf8.TabIndex = 4;
            this.rb_utf8.Text = "View as UTF8";
            this.rb_utf8.UseVisualStyleBackColor = true;
            this.rb_utf8.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_ascii
            // 
            this.rb_ascii.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_ascii.AutoSize = true;
            this.rb_ascii.Location = new System.Drawing.Point(593, 33);
            this.rb_ascii.Name = "rb_ascii";
            this.rb_ascii.Size = new System.Drawing.Size(87, 17);
            this.rb_ascii.TabIndex = 2;
            this.rb_ascii.Text = "View as Ascii";
            this.rb_ascii.UseVisualStyleBackColor = true;
            this.rb_ascii.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_hex
            // 
            this.rb_hex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_hex.AutoSize = true;
            this.rb_hex.Checked = true;
            this.rb_hex.Location = new System.Drawing.Point(593, 10);
            this.rb_hex.Name = "rb_hex";
            this.rb_hex.Size = new System.Drawing.Size(84, 17);
            this.rb_hex.TabIndex = 1;
            this.rb_hex.TabStop = true;
            this.rb_hex.Text = "View as Hex";
            this.rb_hex.UseVisualStyleBackColor = true;
            this.rb_hex.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
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
            this.textBox.TabIndex = 0;
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
            // rb_utf32
            // 
            this.rb_utf32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_utf32.AutoSize = true;
            this.rb_utf32.Location = new System.Drawing.Point(593, 102);
            this.rb_utf32.Name = "rb_utf32";
            this.rb_utf32.Size = new System.Drawing.Size(98, 17);
            this.rb_utf32.TabIndex = 5;
            this.rb_utf32.Text = "View as UTF32";
            this.rb_utf32.UseVisualStyleBackColor = true;
            this.rb_utf32.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_utf7
            // 
            this.rb_utf7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_utf7.AutoSize = true;
            this.rb_utf7.Location = new System.Drawing.Point(593, 56);
            this.rb_utf7.Name = "rb_utf7";
            this.rb_utf7.Size = new System.Drawing.Size(92, 17);
            this.rb_utf7.TabIndex = 3;
            this.rb_utf7.Text = "View as UTF7";
            this.rb_utf7.UseVisualStyleBackColor = true;
            this.rb_utf7.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_unicode
            // 
            this.rb_unicode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_unicode.AutoSize = true;
            this.rb_unicode.Location = new System.Drawing.Point(593, 127);
            this.rb_unicode.Name = "rb_unicode";
            this.rb_unicode.Size = new System.Drawing.Size(105, 17);
            this.rb_unicode.TabIndex = 6;
            this.rb_unicode.Text = "View as Unicode";
            this.rb_unicode.UseVisualStyleBackColor = true;
            this.rb_unicode.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Location = new System.Drawing.Point(590, 426);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(108, 23);
            this.btn_close.TabIndex = 9;
            this.btn_close.Text = "&Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // btn_asString
            // 
            this.btn_asString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_asString.Location = new System.Drawing.Point(590, 397);
            this.btn_asString.Name = "btn_asString";
            this.btn_asString.Size = new System.Drawing.Size(108, 23);
            this.btn_asString.TabIndex = 8;
            this.btn_asString.Text = "View as &String";
            this.btn_asString.UseVisualStyleBackColor = true;
            this.btn_asString.Click += new System.EventHandler(this.AsString_Click);
            // 
            // BinaryViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.btn_asString);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.rb_unicode);
            this.Controls.Add(this.rb_utf32);
            this.Controls.Add(this.rb_utf7);
            this.Controls.Add(this.rb_image);
            this.Controls.Add(this.rb_utf8);
            this.Controls.Add(this.rb_ascii);
            this.Controls.Add(this.rb_hex);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.textBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 250);
            this.Name = "BinaryViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Binary Viewer";
            this.Shown += new System.EventHandler(this.BinaryViewForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_image;
        private System.Windows.Forms.RadioButton rb_utf8;
        private System.Windows.Forms.RadioButton rb_ascii;
        private System.Windows.Forms.RadioButton rb_hex;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.RadioButton rb_utf32;
        private System.Windows.Forms.RadioButton rb_utf7;
        private System.Windows.Forms.RadioButton rb_unicode;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_asString;
    }
}