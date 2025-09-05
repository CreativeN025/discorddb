using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discorddb
{
    internal class MessageSource
    {
        public long ID { get; set; }
        public string Contents { get; set; }
        public DateTime Timestamp { get; set; }
        public string id { get; set; } //channelId
        public string Attachments { get; set; }
    }
}
