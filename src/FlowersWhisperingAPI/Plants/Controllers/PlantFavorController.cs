using FlowersWhisperingAPI.Plants.Services.Interface;
using FlowersWhisperingAPI.Plants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Plants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantFavorController(IPlantFavorService plantFavorService) : ControllerBase
    {
        private readonly IPlantFavorService _plantFavorService = plantFavorService;

        [HttpGet("findFavors")]
        public IActionResult GetFavorPlants (int userId)
        {
            var FavorPlants = _plantFavorService.GetFavorPlants(userId);
            if (FavorPlants == null )
            {
                return BadRequest("获取失败"); // 返回 400 Bad Request
            }
            if (!FavorPlants.Any())
            {
                return Ok("您还没有收藏任何植物"); // 返回 200 OK 和提示信息
            }
            return Ok(FavorPlants); // 返回 200 OK 和 latestPlants
        }

        [HttpPost("addFavor")]
        public IActionResult AddFavorPlant (int userId, int plantId)
        {
            var result = _plantFavorService.AddFavorPlant(userId, plantId);
            if (result)
            {
                return Ok("添加成功"); // 返回 200 OK 和提示信息
            }
            return BadRequest("添加失败"); // 返回 400 Bad Request
        }

        [HttpDelete("deleteFavor")]
        public IActionResult DeleteFavorPlant (int userId, int plantId)
        {
            var result = _plantFavorService.DeleteFavorPlant(userId, plantId);
            if (result)
            {
                return Ok("删除成功"); // 返回 200 OK 和提示信息
            }
            return BadRequest("删除失败"); // 返回 400 Bad Request
        }
    }   
}