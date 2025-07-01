using FlowersWhisperingAPI.Plants.Services.Interface;
using FlowersWhisperingAPI.Plants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Plants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantContController(IPlantContService plantContService) : ControllerBase
    {
        private readonly IPlantContService _plantContService = plantContService;

        [HttpGet("userContributes")]
        public IActionResult GetContPlants (int userId)
        {
            var ContPlants = _plantContService.GetContPlants(userId);
            if (ContPlants == null )
            {
                return BadRequest("获取失败"); // 返回 400 Bad Request
            }
            if (!ContPlants.Any())
            {
                return Ok("您还没有贡献过任何植物"); // 返回 200 OK 和提示信息
            }
            return Ok(ContPlants); // 返回 200 OK 和 latestPlants
        }

        [HttpPost("addContribute")]
        public IActionResult AddContPlant (int userId, string plantName, string reviewContent)
        {
            if (_plantContService.AddContPlant(userId, plantName, reviewContent))
            {
                return Ok("添加成功"); // 返回 200 OK 和提示信息
            }
            return BadRequest("添加失败"); // 返回 400 Bad Request
        }
    }   
}