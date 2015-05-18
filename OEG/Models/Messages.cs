using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OEG.Models
{
    public enum Importance
    {
        Success,
        Error,
        Warning,
        Info
    };

    public class MyMessage
    {
        public MyMessage()
        {
        }

        public MyMessage(string message, Importance i)
        {
            Message = message;
            Im = i;
        }
        public string Message { set; get; }
        public Importance Im { set; get; }
    }
}