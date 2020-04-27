using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewPollController : ControllerBase
    {

        readonly INewPollDomainModel NewPollDomainModel;

        public NewPollController(INewPollDomainModel newPollDomainModel)
        {
            NewPollDomainModel = newPollDomainModel;
        }

        [HttpGet]
        [Route("GetLastPoll")]
        public string GetLastPollId() => NewPollDomainModel.GetLastPoll();

        [HttpPost]
        [Route("AddPoll")]
        public void AddPoll(Poll poll) => NewPollDomainModel.AddPoll(poll);

        [HttpPost]
        [Route("AddAnswers")]
        public void AddAnswers(List<Answer> answers) => NewPollDomainModel.AddAnswers(answers);

        [HttpGet]
        [Route("GetPollAuthor")]
        [Authorize]
        public string GetPollAuthor()
        {
            return NewPollDomainModel.GetPollAuthor(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}