using Microsoft.VisualBasic;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
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
            List<MessageContent> content = readjson(TB_SelectedFolder.Text);
            int i = 1;
            int percentage = 0;
            int saveinterval = 0;
            foreach (MessageContent contentItem in content)
            {
                createinitialstring(command);
                string add = "";
                //Create add string (id,content,date,channelId,attachments)

                saveinterval = Math.Max(1, content.Count / 10);
                add += $"(@Id{i}, @Content{i}, @Date{i}, @ChannelId{i},@ChannelType{i}, @Attachments{i})";
                if (i == content.Count -1 || (i % saveinterval == 0 && i != 0))
                {
                    add += ";";
                }
                else { add += ","; }
              addvalues(command, contentItem,add,i);
                if (i % saveinterval == 0 && i!=0)
                {
                    percentage = Convert.ToInt32(double.Floor(((double)i / (double)content.Count) * 100));
                    Debug.WriteLine($"{i} / {content.Count} = {percentage}");
                    pB_transfer.Value = percentage;
             
                    savedata(command);
                    command.CommandText="";
                }
                i++;
            }
            savedata(command);
        }
        private void addvalues(SQLiteCommand command,MessageContent contentItem,string add,int i)
        {
            command.CommandText += add;
            command.Parameters.AddWithValue($"@Id{i}", contentItem.Id);
            command.Parameters.AddWithValue($"@Content{i}", contentItem.Content);
            command.Parameters.AddWithValue($"@Date{i}", contentItem.Date);
            command.Parameters.AddWithValue($"@ChannelId{i}", contentItem.ChannelId);
            command.Parameters.AddWithValue($"@ChannelType{i}", contentItem.ChannelType);

            command.Parameters.AddWithValue($"@Attachments{i}", contentItem.Attachments);
        }
        private void savedata(SQLiteCommand command)
        {
            if(command.CommandText == "") { return; }
            //Debug.WriteLine(command.CommandText);
            Clipboard.SetText(command.CommandText);
            DatabaseHelper.UpdateDatabase(command);
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
