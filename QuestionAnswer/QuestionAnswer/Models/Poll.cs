using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.Models
{
    public class Poll
    {

        public int? ID { get; set; }
        public int? UserID { get; set; }
        public byte? VotesCount { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsActive { get; set; }
        public bool? CanAddAnswers { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }

    }
}
