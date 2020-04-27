using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuestionAnswer.DomainModels.Interfaces
{
    public interface ICabinetDomainModel
    {

        public string GetUserPolls(string id);

    }
}
