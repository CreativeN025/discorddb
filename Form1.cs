using Microsoft.VisualBasic;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace discorddb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private async void BT_Confirm_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(TB_SelectedFolder.Text);
            pB_transfer.Enabled = true;

            SQLiteCommand command = DatabaseHelper.ConnectToDatabase(DatabaseHelper.getConnectionString());
            StringBuilder sB = new StringBuilder();

            int i = 0;
            int percentage = 0;
            string[] directories = Directory.GetDirectories(TB_SelectedFolder.Text);
            for (int j = 0; j < directories.Length; j++)
            {


                List<MessageContent> content = readjson(directories[j]);
                int contentlength = content.Count;
                int saveinterval = content.Count > 5000 ? content.Count / 10 : content.Count;
                //string add = "";
                command.CommandText = "";
                i = 1;
                foreach (MessageContent contentItem in content)
                {
                    if (command.CommandText == "")
                    {
                        createinitialstring(command);
                    }
                    //Create add string (id,content,date,channelId,attachments)

                    sB.Append($"(@Id{i}, @Content{i}, @Date{i}, @ChannelId{i},@ChannelType{i}, @Attachments{i})");
                    if (i % saveinterval == 0 && i != 0)
                    {
                        sB.Append(";");
                    }
                    else { sB.Append(","); }

                    #region
                    command.CommandText += sB.ToString();
                    command.Parameters.AddWithValue($"@Id{i}", contentItem.Id);
                    command.Parameters.AddWithValue($"@Content{i}", contentItem.Content);
                    command.Parameters.AddWithValue($"@Date{i}", contentItem.Date);
                    command.Parameters.AddWithValue($"@ChannelId{i}", contentItem.ChannelId);
                    command.Parameters.AddWithValue($"@ChannelType{i}", contentItem.ChannelType);
                    command.Parameters.AddWithValue($"@Attachments{i}", contentItem.Attachments);
                    sB.Clear();
                    #endregion

                    if (i % saveinterval == 0 && i != 0 || i == contentlength)
                    {
                        percentage = Convert.ToInt32(double.Floor(((double)i / (double)contentlength) * 100));
                        //Debug.WriteLine($"{i} / {contentlength} = {percentage}");
                        pB_transfer.Value = percentage;

                        savedata(command);
                    }
                    i++;
                }
                savedata(command);
                Console.Beep();

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
    }
}
