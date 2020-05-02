using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Models;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CabinetController : ControllerBase
    {

        readonly ICabinetDomainModel CabinetDomainModel;
        public CabinetController(ICabinetDomainModel cabinetDomainModel) => CabinetDomainModel = cabinetDomainModel;

        [HttpGet]
        [Route("GetUserPolls")]
        public Dictionary<int, User>.ValueCollection GetUserPolls()
        {
            return CabinetDomainModel.GetUserPolls(User.FindFirst(ClaimTypes.NameIdentifier).Value).Values;
        }

        [HttpGet]
        [Route("GetUsername")]
        public string GetUsername() => CabinetDomainModel.GetUsername(Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

    }
}