using FlowersWhisperingAPI.Plants.Mappers;
using FlowersWhisperingAPI.Plants.Models;
using FlowersWhisperingAPI.Plants.Services.Interface;

namespace FlowersWhisperingAPI.Plants.Services
{
    public class PlantContService(PlantContMapper ContMapper) : IPlantContService
    {
        private readonly PlantContMapper _ContMapper = ContMapper;

        public List<PlantWithReview> GetContPlants (int userId)
        {
            return _ContMapper.GetContPlants(userId);
        }

        public bool AddContPlant (int userId, string plantName, string reviewContent)
        {
            return _ContMapper.AddContPlant(userId, plantName, reviewContent);
        }
        
    }
}