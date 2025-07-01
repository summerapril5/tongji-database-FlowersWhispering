using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommuniScoreController(ICommuniScoreService communiScoreService) : ControllerBase
    {
        private readonly ICommuniScoreService _communiScoreService = communiScoreService;

        [HttpGet("score/rank")]
        public IActionResult RankScores()
        {
            return Ok(_communiScoreService.RankScores());
        }

        [HttpGet("score/user")]
        public IActionResult GetUserScore(int userId)
        {
            return Ok(_communiScoreService.GetUserScore(userId));
        }


    }
}