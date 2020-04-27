using Newtonsoft.Json;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.DomainModels
{
    public class MainDomainModel : IMainDomainModel
    {

        readonly IMainRepository MainRepository;

        public MainDomainModel(IMainRepository mainRepository)
        {
            MainRepository = mainRepository;
        }

        public string GetPolls()
        {
            var pollList = MainRepository.GetPolls().Values;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(pollList, settings);
        }

    }
}
