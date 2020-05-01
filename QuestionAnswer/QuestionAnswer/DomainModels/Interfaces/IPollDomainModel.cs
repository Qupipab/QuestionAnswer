using QuestionAnswer.Models;
using System;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface IPollDomainModel
    {

        public Dictionary<int, Poll> GetPoll(string link);
        string Vote(Vote vote, string userId);
        string AddAnswer(Answer answer, string id);

    }
}
