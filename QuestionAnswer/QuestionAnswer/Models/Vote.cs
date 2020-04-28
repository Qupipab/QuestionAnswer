using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.Models
{
    public class Vote
    {

        public int PollID { get; set; }
        public List<UserAnswer> UserVotes { get; set; }

    }
}
