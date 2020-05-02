using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels
{
    public class CabinetDomainModel : ICabinetDomainModel
    {

        readonly ICabinetRepository CabinetRepository;

        public CabinetDomainModel(ICabinetRepository cabinetRepository) => CabinetRepository = cabinetRepository;
        
        public Dictionary<int, User> GetUserPolls(string id) => CabinetRepository.GetUserPolls(id);
        public string GetUsername(int id) => CabinetRepository.GetUsername(id);

    }
}
