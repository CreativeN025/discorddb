using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discorddb
{
    internal class Analysis
    {
        List<string> words = new List<string>();

        string path = "";

        public Analysis(string pPath)
        {
            path = pPath;
        }
        public void Countwords()
        {
            var dic = getData("SELECT Content,Date,ChannelId FROM Message");
            if (dic != null)
            {
                logdic(dic);
                MessageBox.Show("Analysis complete");

            }
            else
            {
                MessageBox.Show("No data found");
            }
        }
        public static void logdic(Dictionary<string, int> dic)
        {

            StringBuilder sb = new StringBuilder();
            foreach (var pair in dic.OrderByDescending(kvp => kvp.Value))
            {
                sb.AppendLine($"{pair.Key} = {pair.Value}");

            }
            using (StreamWriter sw = File.CreateText(@"..\..\Files\counter.txt"))
            {
                sw.WriteLine(sb);

            }

        }
        public static Dictionary<string, int> getData(string query)
        {

            using (SQLiteConnection connection = new SQLiteConnection(DatabaseHelper.getConnectionString()))
            {
                connection.Open();

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Debug.WriteLine("No rows found.");
                            return null;
                        }
                        Dictionary<string, int> dic = new Dictionary<string, int>();
                        int i = 0;
                        int words = 0;
                        while (reader.Read())
                        {

                            string content = reader["Content"]?.ToString();
                            string date = reader["Date"]?.ToString(); // or use DateTime.Parse if you're sure it's valid
                            string channelId = reader["ChannelId"]?.ToString();
                            foreach (string word in content.Trim().ToLower().Split(" "))
                            {
                                if (dic.ContainsKey(word))
                                {
                                    dic[word] += 1;
                                    words++;
                                }
                                else { dic.Add(word, 1); }

                            }

                        }
                        return dic;

                    }
                }

            }

        }
    }
}
