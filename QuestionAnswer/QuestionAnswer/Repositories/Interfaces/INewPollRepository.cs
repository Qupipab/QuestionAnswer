using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface INewPollRepository
    {

        public string GetLastPoll();
        public void AddPoll(Poll poll);
        public void AddAnswers(List<Answer> answers);
        public List<dynamic> GetPollAuthor(string id);

    }
}
