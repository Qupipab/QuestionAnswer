using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Author { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public byte VotesCount { get; set; }
        public float GeneralVotesCount { get; set; }
        public bool CanAddAnswers { get; set; }
        public string Link { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CloseDate { get; set; }
        public IList<Answer> Answers { get; set; }

    }
}
