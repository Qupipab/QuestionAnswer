using Newtonsoft.Json;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace QuestionAnswer.DomainModels
{
    public class NewPollDomainModel : INewPollDomainModel
    {

        readonly INewPollRepository NewPollRepository;

        public NewPollDomainModel(INewPollRepository newPollRepository) => NewPollRepository = newPollRepository;
        
        public int GetLastPoll() => NewPollRepository.GetLastPoll();

        public string GetPollAuthor(string id) => JsonConvert.SerializeObject(NewPollRepository.GetPollAuthor(id));

        public void AddPoll(Poll poll) 
        {
            Review.NullReview(poll);
            if (poll.Link == "") poll.Link += (GetLastPoll() + 1).ToString();
            NewPollRepository.AddPoll(poll);
        } 

        public void AddAnswers(List<Answer> answers, int userid) => NewPollRepository.AddAnswers(answers, userid, GetLastPoll());
        

    }
}
