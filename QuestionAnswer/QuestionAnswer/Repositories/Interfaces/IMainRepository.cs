using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface IMainRepository
    {

        Dictionary<int, User> GetPolls();

    }
}
