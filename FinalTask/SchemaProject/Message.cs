using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class MessageRequest
    {
        public int RecevierId { get; set; }
        public string MessageContent { get; set; }
    }
    public class MessageResponse
    {
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string MessageContent { get; set; }
    }
}
