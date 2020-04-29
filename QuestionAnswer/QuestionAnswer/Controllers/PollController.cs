﻿using System;
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

        [HttpPost]
        [Route("AddAnswer")]
        public string AddAnswer(Answer answer) => PollDomainModel.AddAnswer(answer, User.FindFirst(ClaimTypes.NameIdentifier).Value);

    }
}