﻿using System;
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

        public PollDomainModel(IPollRepository pollRepository) => PollRepository = pollRepository;

        public Dictionary<int, Poll> GetPoll(string link) => PollRepository.GetPoll(link);
        public Dictionary<int, AnswerVote> UserVotes(string link) => PollRepository.UserVotes(link);

        public bool Vote(Vote vote, string userId)
        {
            Review.NullReview(vote);
            if (!PollRepository.CheckUser(vote.PollID, userId))
            {
                foreach (var item in vote.UserVotes)
                {
                    item.UserID = Int32.Parse(userId);
                    PollRepository.Vote(item);
                }
                return true;
            }
            else return false;
        }

        public string AddAnswer(Answer answer, string id)
        {
            Review.NullReview(answer);
            answer.CreatorID = Int32.Parse(id);
            PollRepository.AddAnswer(answer);
            return PollRepository.GetLastAnswerID();
        }

        public void ClosePoll(int id) => PollRepository.ClosePoll(id);

        public void InActivePoll(int id) => PollRepository.InActivePoll(id);

    }
}
