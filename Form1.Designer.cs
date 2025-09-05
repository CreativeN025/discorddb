using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace discorddb
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            BT_OpenFolder = new Button();
            TB_SelectedFolder = new TextBox();
            BT_Confirm = new Button();
            label1 = new Label();
            pB_transfer = new ProgressBar();
            label2 = new Label();
            SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.SelectedPath = "C:\\Users\\liams\\Downloads";
            // 
            // BT_OpenFolder
            // 
            BT_OpenFolder.BackColor = SystemColors.AppWorkspace;
            BT_OpenFolder.Location = new Point(508, 28);
            BT_OpenFolder.Name = "BT_OpenFolder";
            BT_OpenFolder.Size = new Size(110, 32);
            BT_OpenFolder.TabIndex = 0;
            BT_OpenFolder.Text = "Select Folder";
            BT_OpenFolder.UseVisualStyleBackColor = false;
            BT_OpenFolder.Click += BT_OpenFolder_Click;
            // 
            // TB_SelectedFolder
            // 
            TB_SelectedFolder.Location = new Point(126, 29);
            TB_SelectedFolder.Name = "TB_SelectedFolder";
            TB_SelectedFolder.Size = new Size(376, 27);
            TB_SelectedFolder.TabIndex = 1;
            // 
            // BT_Confirm
            // 
            BT_Confirm.BackColor = Color.YellowGreen;
            BT_Confirm.ForeColor = SystemColors.ControlText;
            BT_Confirm.Location = new Point(624, 28);
            BT_Confirm.Name = "BT_Confirm";
            BT_Confirm.Size = new Size(99, 32);
            BT_Confirm.TabIndex = 2;
            BT_Confirm.Text = "Confirm";
            BT_Confirm.UseVisualStyleBackColor = false;
            BT_Confirm.Click += BT_Confirm_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 34);
            label1.Name = "label1";
            label1.Size = new Size(108, 20);
            label1.TabIndex = 3;
            label1.Text = "SelectedFolder";
            // 
            // pB_transfer
            // 
            pB_transfer.Location = new Point(208, 102);
            pB_transfer.Name = "pB_transfer";
            pB_transfer.Size = new Size(370, 29);
            pB_transfer.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(106, 111);
            label2.Name = "label2";
            label2.Size = new Size(87, 20);
            label2.TabIndex = 5;
            label2.Text = "Progressbar";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(pB_transfer);
            Controls.Add(label1);
            Controls.Add(BT_Confirm);
            Controls.Add(TB_SelectedFolder);
            Controls.Add(BT_OpenFolder);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private Button BT_OpenFolder;
        private TextBox TB_SelectedFolder;
        private Button BT_Confirm;
        private Label label1;
        private ProgressBar pB_transfer;
        private Label label2;
    }
}
