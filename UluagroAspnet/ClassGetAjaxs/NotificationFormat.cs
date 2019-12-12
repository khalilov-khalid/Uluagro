using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class NotificationFormat
    {
        public int Objectid { get; set; }
        public string SenderName { get; set; }
        public string messagename { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }
        public bool WorkStatus { get; set; }
        public DateTime MessageSendedTime { get; set; }

    }
}