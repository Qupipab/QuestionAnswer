using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface ICabinetRepository
    {

        public Dictionary<int, User> GetUserPolls(string id);

    }
}
