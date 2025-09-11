using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace discorddb.Objects
{
    public class MessageContent
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string ChannelId { get; set; }
        public string ChannelType { get; set; }
        public string Attachments { get; set; }
    }
}
