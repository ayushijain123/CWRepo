using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBack.Models
{
    public class questioncontent
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public string AnswerType { get; set; }
        public int ColumnCount { get; set; }
    }
}