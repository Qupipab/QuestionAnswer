using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface INewPollDomainModel
    {

        public string GetLastPoll();
        public void AddPoll(Poll poll);
        public void AddAnswers(List<Answer> answers);
        public string GetPollAuthor(string id);

    }
}
