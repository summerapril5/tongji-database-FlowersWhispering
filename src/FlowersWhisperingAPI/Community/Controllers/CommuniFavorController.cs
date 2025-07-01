using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services;
using FlowersWhisperingAPI.Community.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommuniFavorController(ICommuniFavorService communiFavorService) : ControllerBase
    {
        private readonly ICommuniFavorService _communiFavorService = communiFavorService;

        [HttpPost("favor/add")]
        public void AddFavor([FromBody] FavorDTO favordto)
        {
            _communiFavorService.AddFavor(favordto);
        }

        [HttpDelete("favor/delete")]
        public void DeleteFavor(int favorId)
        {
            _communiFavorService.DeleteFavor(favorId);
        }
        //本用户收藏
        [HttpGet("favor/get")]
        public IActionResult GetAllFavor(int userId)
        {
            return Ok(_communiFavorService.GetAllFavor(userId));
        }


    }
}