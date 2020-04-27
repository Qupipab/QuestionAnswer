using Newtonsoft.Json;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.DomainModels
{
    public class CabinetDomainModel : ICabinetDomainModel
    {

        readonly ICabinetRepository CabinetRepository;

        public CabinetDomainModel(ICabinetRepository cabinetRepository)
        {
            CabinetRepository = cabinetRepository;
        }

        public string GetUserPolls(string id)
        {
            var userPollList = CabinetRepository.GetUserPolls(id).Values;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(userPollList, settings);
        }

    }
}
