using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels
{
    public class MainDomainModel : IMainDomainModel
    {

        readonly IMainRepository MainRepository;

        public MainDomainModel(IMainRepository mainRepository) => MainRepository = mainRepository;
        
        public Dictionary<int, User> GetPolls() => MainRepository.GetPolls();
        

    }
}
