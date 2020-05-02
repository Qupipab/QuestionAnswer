using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface INewPollRepository
    {

        int GetLastPoll();
        void AddPoll(Poll poll);
        void AddAnswers(List<Answer> answers, int userId, int lastPoll);
        List<dynamic> GetPollAuthor(string id);
        void UpdateAnswers(List<Answer> answers, int userId, int lastPoll);
        void UpdatePoll(Poll p);

    }
}
