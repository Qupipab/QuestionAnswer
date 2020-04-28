using QuestionAnswer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface IPollRepository
    {

        public Dictionary<int, Poll> GetPoll(int id);
        public void Vote(UserAnswer vote);
        public bool CheckUser(int pollID, string userId);

    }
}
