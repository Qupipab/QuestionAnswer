using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using QuestionAnswer.Repositories.Interfaces;

namespace QuestionAnswer.DomainModels
{
    public class PollDomainModel : IPollDomainModel
    {

        readonly IPollRepository PollRepository;

        public PollDomainModel(IPollRepository pollRepository)
        {
            PollRepository = pollRepository;
        }

        public string GetPoll(int id)
        {
            var pollList = PollRepository.GetPoll(id).Values;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(pollList, settings);
        }

        public string Vote(Vote vote, string userId)
        {
            if (!PollRepository.CheckUser(vote.PollID, userId))
            {
                foreach (var item in vote.UserVotes)
                {
                    item.UserID = Int32.Parse(userId);
                    PollRepository.Vote(item);
                }
                return "1";
            }
            else return "-1";
        }

        public string AddAnswer(Answer answer, string id)
        {
            answer.CreatorID = Int32.Parse(id);
            PollRepository.AddAnswer(answer);
            return PollRepository.GetLastAnswerID();
        }

    }
}
