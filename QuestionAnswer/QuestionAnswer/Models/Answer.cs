using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.Models
{
    public class Answer
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public int CreatorID { get; set; }
        public int PollID { get; set; }

    }
}
