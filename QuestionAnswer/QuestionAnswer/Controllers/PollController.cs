using System;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        readonly IPollDomainModel PollDomainModel;

        public PollController(IPollDomainModel pollDomainModel)
        {
            PollDomainModel = pollDomainModel;
        }

        [HttpGet]
        [Route("GetPoll/{id}")]
        public string GetPoll(int id) => PollDomainModel.GetPoll(id);
        

        [HttpPost]
        [Route("Vote")]
        public string Vote(Vote vote) => PollDomainModel.Vote(vote, User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
    }
}