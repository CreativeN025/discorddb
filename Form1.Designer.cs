using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;
using discorddb.Objects;

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
            components = new System.ComponentModel.Container();
            folderBrowserDialog1 = new FolderBrowserDialog();
            BT_OpenFolder = new Button();
            TB_SelectedFolder = new TextBox();
            BT_Confirm = new Button();
            label1 = new Label();
            pB_transfer = new ProgressBar();
            lb_pb = new Label();
            pB_total = new ProgressBar();
            cB_sound = new CheckBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            cB_mode = new ComboBox();
            label2 = new Label();
            cB_progress = new CheckBox();
            messageContentBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)messageContentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.SelectedPath = "C:\\Users\\liams\\Downloads";
            // 
            // BT_OpenFolder
            // 
            BT_OpenFolder.BackColor = SystemColors.AppWorkspace;
            BT_OpenFolder.Location = new Point(518, 26);
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
            BT_Confirm.Location = new Point(634, 26);
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
            pB_transfer.Location = new Point(212, 392);
            pB_transfer.Name = "pB_transfer";
            pB_transfer.Size = new Size(329, 20);
            pB_transfer.TabIndex = 4;
            pB_transfer.Visible = false;
            // 
            // lb_pb
            // 
            lb_pb.AutoSize = true;
            lb_pb.Location = new Point(61, 348);
            lb_pb.Name = "lb_pb";
            lb_pb.Size = new Size(87, 20);
            lb_pb.TabIndex = 5;
            lb_pb.Text = "Progressbar";
            lb_pb.Visible = false;
            // 
            // pB_total
            // 
            pB_total.Location = new Point(171, 339);
            pB_total.Name = "pB_total";
            pB_total.Size = new Size(394, 29);
            pB_total.TabIndex = 6;
            pB_total.Visible = false;
            // 
            // cB_sound
            // 
            cB_sound.AutoSize = true;
            cB_sound.Location = new Point(599, 341);
            cB_sound.Name = "cB_sound";
            cB_sound.Size = new Size(109, 24);
            cB_sound.TabIndex = 7;
            cB_sound.Text = "Beep sound";
            cB_sound.UseVisualStyleBackColor = true;
            cB_sound.Visible = false;
            // 
            // cB_mode
            // 
            cB_mode.AllowDrop = true;
            cB_mode.FormattingEnabled = true;
            cB_mode.ImeMode = ImeMode.Off;
            cB_mode.Items.AddRange(new object[] { "Search", "Analysis" });
            cB_mode.Location = new Point(243, 62);
            cB_mode.Name = "cB_mode";
            cB_mode.Size = new Size(151, 28);
            cB_mode.TabIndex = 9;
            cB_mode.Text = "Select mode";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(145, 65);
            label2.Name = "label2";
            label2.Size = new Size(92, 20);
            label2.TabIndex = 10;
            label2.Text = "Select mode";
            // 
            // cB_progress
            // 
            cB_progress.AutoSize = true;
            cB_progress.Location = new Point(421, 67);
            cB_progress.Name = "cB_progress";
            cB_progress.Size = new Size(190, 24);
            cB_progress.TabIndex = 11;
            cB_progress.Text = "View progressbar (slow)";
            cB_progress.UseVisualStyleBackColor = true;
            // 
            // messageContentBindingSource
            // 
            messageContentBindingSource.DataSource = typeof(MessageContent);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(758, 450);
            Controls.Add(cB_progress);
            Controls.Add(label2);
            Controls.Add(cB_mode);
            Controls.Add(cB_sound);
            Controls.Add(pB_total);
            Controls.Add(lb_pb);
            Controls.Add(pB_transfer);
            Controls.Add(label1);
            Controls.Add(BT_Confirm);
            Controls.Add(TB_SelectedFolder);
            Controls.Add(BT_OpenFolder);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)messageContentBindingSource).EndInit();
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
        private Label lb_pb;
        private ProgressBar pB_total;
        private CheckBox cB_sound;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ComboBox cB_mode;
        private Label label2;
        private CheckBox cB_progress;
        private BindingSource messageContentBindingSource;
    }
}
