using QuestionAnswer.Models;
using System.Collections.Generic;

namespace QuestionAnswer.Repositories.Interfaces
{
    public interface ICabinetRepository
    {

        Dictionary<int, User> GetUserPolls(string id);
        public string GetUsername(int id);

    }
}
