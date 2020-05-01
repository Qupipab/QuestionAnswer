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
        public bool IsPrivate { get; set; }
        public bool IsDraft { get; set; }
        public bool IsClosed { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnon { get; set; }
        public string Link { get; set; }
        public DateTime CloseDate { get; set; }
        public List<Answer> Answers { get; set; }

    }
}
