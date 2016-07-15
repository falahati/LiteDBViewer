namespace LiteDBViewer
{
    partial class StringViewForm
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.rb_String = new System.Windows.Forms.RadioButton();
            this.rb_XML = new System.Windows.Forms.RadioButton();
            this.rb_Json = new System.Windows.Forms.RadioButton();
            this.rb_HTML = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.SystemColors.Window;
            this.textBox.Font = new System.Drawing.Font("Courier New", 9F);
            this.textBox.Location = new System.Drawing.Point(12, 10);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(572, 439);
            this.textBox.TabIndex = 0;
            this.textBox.WordWrap = false;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowNavigation = false;
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(12, 12);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(572, 439);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.Visible = false;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // rb_String
            // 
            this.rb_String.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_String.AutoSize = true;
            this.rb_String.Checked = true;
            this.rb_String.Location = new System.Drawing.Point(600, 12);
            this.rb_String.Name = "rb_String";
            this.rb_String.Size = new System.Drawing.Size(92, 17);
            this.rb_String.TabIndex = 2;
            this.rb_String.TabStop = true;
            this.rb_String.Text = "View as String";
            this.rb_String.UseVisualStyleBackColor = true;
            this.rb_String.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_XML
            // 
            this.rb_XML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_XML.AutoSize = true;
            this.rb_XML.Location = new System.Drawing.Point(600, 35);
            this.rb_XML.Name = "rb_XML";
            this.rb_XML.Size = new System.Drawing.Size(87, 17);
            this.rb_XML.TabIndex = 3;
            this.rb_XML.Text = "View as XML";
            this.rb_XML.UseVisualStyleBackColor = true;
            this.rb_XML.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_Json
            // 
            this.rb_Json.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_Json.AutoSize = true;
            this.rb_Json.Location = new System.Drawing.Point(600, 58);
            this.rb_Json.Name = "rb_Json";
            this.rb_Json.Size = new System.Drawing.Size(87, 17);
            this.rb_Json.TabIndex = 4;
            this.rb_Json.Text = "View as Json";
            this.rb_Json.UseVisualStyleBackColor = true;
            this.rb_Json.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // rb_HTML
            // 
            this.rb_HTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_HTML.AutoSize = true;
            this.rb_HTML.Location = new System.Drawing.Point(600, 81);
            this.rb_HTML.Name = "rb_HTML";
            this.rb_HTML.Size = new System.Drawing.Size(95, 17);
            this.rb_HTML.TabIndex = 5;
            this.rb_HTML.Text = "View as HTML";
            this.rb_HTML.UseVisualStyleBackColor = true;
            this.rb_HTML.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // StringViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 461);
            this.Controls.Add(this.rb_HTML);
            this.Controls.Add(this.rb_Json);
            this.Controls.Add(this.rb_XML);
            this.Controls.Add(this.rb_String);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.webBrowser);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 250);
            this.Name = "StringViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "String Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.RadioButton rb_String;
        private System.Windows.Forms.RadioButton rb_XML;
        private System.Windows.Forms.RadioButton rb_Json;
        private System.Windows.Forms.RadioButton rb_HTML;
    }
}