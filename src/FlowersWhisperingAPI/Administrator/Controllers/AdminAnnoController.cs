using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAnnoController(IAdminAnnoService adminAnnoService) : ControllerBase
    {
        private readonly IAdminAnnoService _adminAnnoService = adminAnnoService;

        [HttpGet("announcement/all")]
        public IActionResult GetAllAnnouncements()
        {
            return Ok(_adminAnnoService.GetAllAnnouncements());
        }

        [HttpPost("announcement/publish")]
        public void PublishAnnouncement([FromBody] AnnoDTO annoDTO)
        {
            _adminAnnoService.AddAnnouncement(annoDTO);
        }

        [HttpDelete("announcement/delete")]
        public void DeleteAnnouncement(int announcementId)
        {
            _adminAnnoService.DeleteAnnouncement(announcementId);
        }
    }
}