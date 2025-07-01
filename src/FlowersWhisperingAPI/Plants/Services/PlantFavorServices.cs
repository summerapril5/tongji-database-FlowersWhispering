using FlowersWhisperingAPI.Plants.Mappers;
using FlowersWhisperingAPI.Plants.Models;
using FlowersWhisperingAPI.Plants.Services.Interface;

namespace FlowersWhisperingAPI.Plants.Services
{
    public class PlantFavorService(PlantFavorMapper FavorMapper) : IPlantFavorService
    {
        private readonly PlantFavorMapper _FavorMapper = FavorMapper;

        public List<Plant> GetFavorPlants (int userId)
        {
            return _FavorMapper.GetFavorPlants(userId);
        }

        public bool AddFavorPlant (int userId, int plantId)
        {
            return _FavorMapper.AddFavorPlant(userId, plantId);
        }
        
        public bool DeleteFavorPlant (int userId, int plantId)
        {
            return _FavorMapper.DeleteFavorPlant(userId, plantId);
        }
    }
}