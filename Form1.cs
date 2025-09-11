using discorddb.Objects;
using Microsoft.VisualBasic;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace discorddb
{
    public partial class Form1 : Form
    {
        DateTime starttime;
        DateTime endtime;
        public Form1()
        {
            InitializeComponent();
            starttime = DateTime.Now;
            setdownloadvisibility(false);
            cB_mode.DropDownStyle = ComboBoxStyle.DropDownList;
        }



        private async void BT_Confirm_Click(object sender, EventArgs e)
        {
            switch (cB_mode.Text)
            {
                case "Search":
                    if (TB_SelectedFolder.Text != "")
                    {
                        setdownloadvisibility(true);
                        await StartAsync();
                        endtime = DateTime.Now;
                        using (StreamWriter sw = File.CreateText(@"..\..\Files\stats.txt"))
                        {
                            sw.WriteLine("start: "+starttime);
                            sw.WriteLine("end: "+endtime);
                        }
                        Application.Exit();
    
                    }
                    else { MessageBox.Show("no folder selected"); }
                    break;

                case "Analysis":
                    Analysis analysis = new Analysis(DatabaseHelper.getConnectionString());
                    analysis.Countwords();
                    break;

                default: MessageBox.Show("Select mode");
                    break;
                
            }
          
        }
        
        private async Task StartAsync()
        {
            string folderPath = TB_SelectedFolder.Text;
            string[] directories = Directory.GetDirectories(folderPath);

            await Task.Run(() =>
            {
                SQLiteCommand command = DatabaseHelper.ConnectToDatabase(DatabaseHelper.getConnectionString());
                StringBuilder sB = new StringBuilder();

                for (int j = 0; j < directories.Length; j++)
                {
                    List<MessageContent> content = readjson(directories[j]);
                    int contentlength = content.Count;
                    int saveinterval = contentlength > 5000 ? contentlength / 10 : contentlength;

                    int i = 1;
                    foreach (MessageContent contentItem in content)
                    {
                        if (string.IsNullOrWhiteSpace(command.CommandText))
                        {
                            createinitialstring(command);
                        }

                        sB.Append($"(@Id{i}, @Content{i}, @Date{i}, @ChannelId{i},@ChannelType{i}, @Attachments{i})");
                        if (i % saveinterval == 0 && i != 0)
                            sB.Append(";");
                        else
                            sB.Append(",");

                        command.CommandText += sB.ToString();
                        command.Parameters.AddWithValue($"@Id{i}", contentItem.Id);
                        command.Parameters.AddWithValue($"@Content{i}", contentItem.Content);
                        command.Parameters.AddWithValue($"@Date{i}", contentItem.Date);
                        command.Parameters.AddWithValue($"@ChannelId{i}", contentItem.ChannelId);
                        command.Parameters.AddWithValue($"@ChannelType{i}", contentItem.ChannelType);
                        command.Parameters.AddWithValue($"@Attachments{i}", contentItem.Attachments);
                        sB.Clear();

                        if (i % Math.Max(saveinterval / 10, 1) == 0)
                        {
                            int percent = Convert.ToInt32(((double)i / contentlength) * 100);
                            UpdateProgressBar(pB_transfer, percent);
                        }

                        if (i % saveinterval == 0 || i == contentlength)
                        {
                            UpdateProgressBar(pB_transfer, Convert.ToInt32(((double)i / contentlength) * 100));
                            savedata(command);
                        }

                        i++;
                    }

                    savedata(command);

                    // Update total progress
                    int totalPercent = Convert.ToInt32(((double)j / directories.Length) * 100);
                    UpdateProgressBar(pB_total, totalPercent);

                    if (cB_sound.Checked)
                        Console.Beep();
                }
            });
        }
        public void setdownloadvisibility(bool b)
        {

            cB_sound.Visible = b;
            pB_transfer.Visible = b;
            pB_total.Visible = b;
            lb_pb.Visible = b;
        }
        private void UpdateProgressBar(ProgressBar bar, int value)
        {
            if(cB_progress.Checked)
            {
                if (bar.InvokeRequired)
                {
                    bar.Invoke(() => bar.Value = Math.Min(bar.Maximum, value));
                }
                else
                {
                    bar.Value = Math.Min(bar.Maximum, value);
                }
            }
           
        }

        private void savedata(SQLiteCommand command)
        {
            if (command.CommandText.TrimEnd().EndsWith(',') == true)
            {
                command.CommandText = command.CommandText.TrimEnd(',') + ";";
            }
            if (command.CommandText == "" || command.CommandText == "INSERT INTO Message (Id,Content,Date,ChannelId,ChannelType,Attachments)  \r\n                VALUES")
            { return; }

            //Debug.WriteLine(command.CommandText);
            //Clipboard.SetText(command.CommandText);
            command.Prepare();
            DatabaseHelper.UpdateDatabase(command);
            command.CommandText = "";
        }

        private void createinitialstring(SQLiteCommand command)
        {
            command.CommandText = $@"
                INSERT OR IGNORE INTO Message (Id,Content,Date,ChannelId,ChannelType,Attachments)  
                VALUES
            ";
            
        }

        private List<MessageContent> readjson(string dir)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new CustomDateTimeConverter() }
            };

            List<MessageSource> source = new List<MessageSource>();
            ChannelSource source2 = new ChannelSource();

            using (StreamReader SR = new StreamReader($@"{dir}\\messages.json"))
            {
                string json = SR.ReadToEnd();
                source = JsonSerializer.Deserialize<List<MessageSource>>(json, options);
            }
            using (StreamReader SR = new StreamReader($@"{dir}\\channel.json"))
            {
                string json = SR.ReadToEnd();
                source2 = JsonSerializer.Deserialize<ChannelSource>(json);
            }



            List<MessageContent> destination = source.Select(d => new MessageContent
            {
                Id = d.ID,
                Content = d.Contents,
                Date = d.Timestamp,
                ChannelId = source2.id, //channelId
                ChannelType = source2.type,
                Attachments = d.Attachments

            }).ToList();


            return destination;
        }
        private void BT_OpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.InitialDirectory = @"C:\\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TB_SelectedFolder.Text = dialog.SelectedPath;
            }
            else { TB_SelectedFolder.Text = ""; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
