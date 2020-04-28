using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MainController : ControllerBase
    {
        readonly IMainDomainModel MainDomainModel;

        public MainController(IMainDomainModel mainDomainModel)
        {
            MainDomainModel = mainDomainModel;
        }

        [HttpGet]
        [Route("GetPubPolls")]
        public string GetPolls()
        {
            return MainDomainModel.GetPolls();
        }

    }
}