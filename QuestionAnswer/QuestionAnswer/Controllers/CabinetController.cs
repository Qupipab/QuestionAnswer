using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnswer.DomainModels.Interfaces;

namespace QuestionAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CabinetController : ControllerBase
    {
        readonly ICabinetDomainModel CabinetDomainModel;
        public CabinetController(ICabinetDomainModel cabinetDomainModel)
        {
            CabinetDomainModel = cabinetDomainModel;
        }

        [HttpGet]
        [Route("GetUserPolls")]
        public string GetUserPolls()
        {
            return CabinetDomainModel.GetUserPolls(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}