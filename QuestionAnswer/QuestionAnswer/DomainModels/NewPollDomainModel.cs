using Newtonsoft.Json;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels
{
    public class NewPollDomainModel : INewPollDomainModel
    {

        readonly INewPollRepository NewPollRepository;

        public NewPollDomainModel(INewPollRepository newPollRepository)
        {
            NewPollRepository = newPollRepository;
        }

        public string GetLastPoll() => NewPollRepository.GetLastPoll();

        public string GetPollAuthor(string id) => JsonConvert.SerializeObject(NewPollRepository.GetPollAuthor(id));

        public void AddPoll(Poll poll) => NewPollRepository.AddPoll(poll);

        public void AddAnswers(List<Answer> answers) => NewPollRepository.AddAnswers(answers);

    }
}
