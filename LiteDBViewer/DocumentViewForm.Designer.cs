namespace LiteDBViewer
{
    partial class DocumentViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentViewForm));
            this.listBox = new System.Windows.Forms.ListBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_json = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.Font = new System.Drawing.Font("Courier New", 9F);
            this.listBox.FormattingEnabled = true;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(12, 11);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(260, 349);
            this.listBox.TabIndex = 0;
            this.listBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_Mouse);
            this.listBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox_Mouse);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Location = new System.Drawing.Point(12, 396);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(260, 23);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "&Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // btn_json
            // 
            this.btn_json.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_json.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_json.Location = new System.Drawing.Point(12, 367);
            this.btn_json.Name = "btn_json";
            this.btn_json.Size = new System.Drawing.Size(260, 23);
            this.btn_json.TabIndex = 1;
            this.btn_json.Text = "&View As String";
            this.btn_json.UseVisualStyleBackColor = true;
            this.btn_json.Click += new System.EventHandler(this.ViewJson_Click);
            // 
            // DocumentViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(284, 431);
            this.Controls.Add(this.btn_json);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.listBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 440);
            this.Name = "DocumentViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Object Viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_json;
    }
}