using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface IMainRepository
    {

        public Dictionary<int, User> GetPolls();

    }
}
