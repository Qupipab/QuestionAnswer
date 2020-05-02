using QuestionAnswer.Models;
using System;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface ICabinetDomainModel
    {

        Dictionary<int, User> GetUserPolls(string id);
        public string GetUsername(int id);

    }
}
