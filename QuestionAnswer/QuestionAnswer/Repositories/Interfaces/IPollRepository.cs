using QuestionAnswer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface IPollRepository
    {

        Dictionary<int, Poll> GetPoll(string link);
        Dictionary<int, AnswerVote> UserVotes(string link);
        void Vote(UserAnswer vote);
        void AddAnswer(Answer answer);
        bool CheckUser(int pollId, string userId);
        string GetLastAnswerID();
        void ClosePoll(int id);
        void InActivePoll(int id);

    }
}
