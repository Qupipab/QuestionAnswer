using System;
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
    [Authorize]
    public class NewPollController : ControllerBase
    {

        readonly INewPollDomainModel NewPollDomainModel;

        public NewPollController(INewPollDomainModel newPollDomainModel) => NewPollDomainModel = newPollDomainModel;
        
        [HttpGet]
        [Route("GetLastPoll")]
        public int GetLastPollId() => NewPollDomainModel.GetLastPoll();

        [HttpPost]
        [Route("AddPoll")]
        public bool AddPoll(Poll poll)
        {
            try
            {
                Review.NullReview(poll);
                poll.UserID = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                NewPollDomainModel.AddPoll(poll);
                NewPollDomainModel.AddAnswers(poll.Answers, poll.UserID);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [HttpPost]
        [Route("UpdatePoll")]
        public bool UpdatePoll(Poll poll)
        {
            try
            {
                Review.NullReview(poll);
                poll.UserID = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                NewPollDomainModel.UpdatePoll(poll);
                NewPollDomainModel.UpdateAnswers(poll.Answers, poll.UserID);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        [HttpPost]
        [Route("AddAnswers")]
        public void AddAnswers(List<Answer> answers)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            NewPollDomainModel.AddAnswers(answers, userId);
        }

        [HttpGet]
        [Route("GetPollAuthor")]
        [Authorize]
        public string GetPollAuthor()
        {
            return NewPollDomainModel.GetPollAuthor(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}