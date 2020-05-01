using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        readonly IMainDomainModel MainDomainModel;

        public MainController(IMainDomainModel mainDomainModel) => MainDomainModel = mainDomainModel;
        
        [HttpGet]
        [Route("GetPubPolls")]
        public Dictionary<int, User>.ValueCollection GetPolls() => MainDomainModel.GetPolls().Values;
        
    }
}