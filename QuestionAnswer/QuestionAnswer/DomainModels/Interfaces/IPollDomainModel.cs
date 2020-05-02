using QuestionAnswer.Models;
using System;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface IPollDomainModel
    {

        public Dictionary<int, Poll> GetPoll(string link);
        public Dictionary<int, AnswerVote> UserVotes(string link);
        public bool Vote(Vote vote, string userId);
        string AddAnswer(Answer answer, string id);
        void ClosePoll(int id);
        void InActivePoll(int id);

    }
}
