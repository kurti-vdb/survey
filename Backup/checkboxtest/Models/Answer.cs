using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class Answer : IEntity
    {
        public int ID { get; set; }
        public string AnswerValue { get; set; }
        public Dictionary<string, string> Answerdictionary = new Dictionary<string, string>();

        //dictionary.Add("subQuestion 1", "subAnswer 1");
        //dictionary.Add("subQuestion 2", "subAnswer 2");

    }
}