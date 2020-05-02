using System;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollController : ControllerBase
    {

        readonly IPollDomainModel PollDomainModel;

        public PollController(IPollDomainModel pollDomainModel) => PollDomainModel = pollDomainModel;

        [HttpGet]
        [Route("GetPoll/{link}")]
        public Dictionary<int, Poll>.ValueCollection GetPoll(string link) => PollDomainModel.GetPoll(link).Values;

        [HttpGet]
        [Route("UserVotes/{link}")]
        public Dictionary<int, AnswerVote>.ValueCollection UserVotes(string link) => PollDomainModel.UserVotes(link).Values;
        
        [HttpPost]
        [Route("Vote")]
        public bool Vote(Vote vote) => PollDomainModel.Vote(vote, User.FindFirst(ClaimTypes.NameIdentifier).Value);

        [HttpPost]
        [Route("AddAnswer")]
        public string AddAnswer(Answer answer) => PollDomainModel.AddAnswer(answer, User.FindFirst(ClaimTypes.NameIdentifier).Value);

        [HttpPost]
        [Route("ClosePoll/{id}")]
        public void ClosePoll(int id) => PollDomainModel.ClosePoll(id);

        [HttpPost]
        [Route("InActive/{id}")]
        public void InActivePoll(int id) => PollDomainModel.InActivePoll(id);

    }
}