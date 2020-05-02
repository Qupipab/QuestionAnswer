using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface INewPollDomainModel
    {

        int GetLastPoll();
        void AddPoll(Poll poll);
        void AddAnswers(List<Answer> answers, int userId);
        string GetPollAuthor(string id);
        void UpdatePoll(Poll poll);
        void UpdateAnswers(List<Answer> answers, int userid);

    }
}
