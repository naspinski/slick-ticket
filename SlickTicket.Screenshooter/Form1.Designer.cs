namespace SlickTicket.Screenshooter
{
    partial class Form1
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
            this.btnSend = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGetImageFromClipboard = new System.Windows.Forms.Button();
            this.btnCreateScreenshot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEnableDraw = new System.Windows.Forms.Button();
            this.comboBox_Color = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox_PenWidth = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(10, 151);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(153, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send Image";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 1);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btnGetImageFromClipboard
            // 
            this.btnGetImageFromClipboard.Location = new System.Drawing.Point(10, 10);
            this.btnGetImageFromClipboard.Name = "btnGetImageFromClipboard";
            this.btnGetImageFromClipboard.Size = new System.Drawing.Size(153, 23);
            this.btnGetImageFromClipboard.TabIndex = 2;
            this.btnGetImageFromClipboard.Text = "Get Image from Clipboard";
            this.btnGetImageFromClipboard.UseVisualStyleBackColor = true;
            this.btnGetImageFromClipboard.Click += new System.EventHandler(this.btnGetImageFromClipboard_Click);
            // 
            // btnCreateScreenshot
            // 
            this.btnCreateScreenshot.Location = new System.Drawing.Point(10, 39);
            this.btnCreateScreenshot.Name = "btnCreateScreenshot";
            this.btnCreateScreenshot.Size = new System.Drawing.Size(153, 23);
            this.btnCreateScreenshot.TabIndex = 3;
            this.btnCreateScreenshot.Text = "Create Screenshot";
            this.btnCreateScreenshot.UseVisualStyleBackColor = true;
            this.btnCreateScreenshot.Click += new System.EventHandler(this.btnCreateScreenshot_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 628);
            this.panel1.TabIndex = 5;
            // 
            // btnEnableDraw
            // 
            this.btnEnableDraw.Image = global::SlickTicket.Screenshooter.Properties.Resources._16_em_pencil;
            this.btnEnableDraw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnableDraw.Location = new System.Drawing.Point(10, 68);
            this.btnEnableDraw.Name = "btnEnableDraw";
            this.btnEnableDraw.Size = new System.Drawing.Size(153, 23);
            this.btnEnableDraw.TabIndex = 6;
            this.btnEnableDraw.Text = "Draw on Screenshot";
            this.btnEnableDraw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnableDraw.UseVisualStyleBackColor = true;
            this.btnEnableDraw.Click += new System.EventHandler(this.btnEnableDraw_Click);
            // 
            // comboBox_Color
            // 
            this.comboBox_Color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Color.Enabled = false;
            this.comboBox_Color.FormattingEnabled = true;
            this.comboBox_Color.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Yellow",
            "Black",
            "White"});
            this.comboBox_Color.Location = new System.Drawing.Point(10, 97);
            this.comboBox_Color.Name = "comboBox_Color";
            this.comboBox_Color.Size = new System.Drawing.Size(153, 21);
            this.comboBox_Color.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.comboBox_PenWidth);
            this.panel2.Controls.Add(this.btnGetImageFromClipboard);
            this.panel2.Controls.Add(this.comboBox_Color);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.btnEnableDraw);
            this.panel2.Controls.Add(this.btnCreateScreenshot);
            this.panel2.Location = new System.Drawing.Point(627, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 186);
            this.panel2.TabIndex = 8;
            // 
            // comboBox_PenWidth
            // 
            this.comboBox_PenWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PenWidth.Enabled = false;
            this.comboBox_PenWidth.FormattingEnabled = true;
            this.comboBox_PenWidth.Items.AddRange(new object[] {
            "1 px",
            "2 px",
            "3 px",
            "4 px",
            "5 px",
            "6 px",
            "7 px",
            "8 px",
            "9 px",
            "10 px"});
            this.comboBox_PenWidth.Location = new System.Drawing.Point(10, 124);
            this.comboBox_PenWidth.Name = "comboBox_PenWidth";
            this.comboBox_PenWidth.Size = new System.Drawing.Size(153, 21);
            this.comboBox_PenWidth.TabIndex = 8;
            this.comboBox_PenWidth.SelectionChangeCommitted += new System.EventHandler(this.comboBox_PenWidth_SelectionChangeCommitted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 642);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Screenshooter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGetImageFromClipboard;
        private System.Windows.Forms.Button btnCreateScreenshot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEnableDraw;
        private System.Windows.Forms.ComboBox comboBox_Color;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBox_PenWidth;
    }
}

