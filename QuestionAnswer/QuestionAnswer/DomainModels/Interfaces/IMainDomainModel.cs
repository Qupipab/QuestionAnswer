using QuestionAnswer.Models;
using System;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface IMainDomainModel
    {

        Dictionary<int, User> GetPolls();

    }
}
