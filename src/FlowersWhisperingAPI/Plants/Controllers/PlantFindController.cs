using FlowersWhisperingAPI.Plants.Services.Interface;
using FlowersWhisperingAPI.Plants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Plants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantFindController(IPlantFindService plantFindService) : ControllerBase
    {
        private readonly IPlantFindService _plantFindService = plantFindService;

        [HttpGet("findId")]
        public IActionResult GetPlantId (string plantName)
        {
            var plantId = _plantFindService.GetPlantId(plantName);
            if (plantId == -1)
            {
                return BadRequest("未找到相关植物"); // 返回 400 Bad Request
            }
            return Ok(plantId); // 返回 200 OK 和 plantId
        }
        [HttpGet("findInfo")]
        public IActionResult GetPlantInfo (string plantName)
        {
            var plantInfo = _plantFindService.GetPlantInfo(plantName);
            if (plantInfo == null)
            {
                return BadRequest("未找到相关植物"); // 返回 400 Bad Request
            }
            return Ok(plantInfo); // 返回 200 OK 和 plantInfo
        }
    }   
}