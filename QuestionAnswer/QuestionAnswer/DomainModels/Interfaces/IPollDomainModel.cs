using QuestionAnswer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface IPollDomainModel
    {

        public string GetPoll(int id);
        public string Vote(Vote vote, string userId);

    }
}
