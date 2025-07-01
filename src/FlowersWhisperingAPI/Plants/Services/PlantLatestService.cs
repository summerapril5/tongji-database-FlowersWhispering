using FlowersWhisperingAPI.Plants.Mappers;
using FlowersWhisperingAPI.Plants.Models;
using FlowersWhisperingAPI.Plants.Services.Interface;

namespace FlowersWhisperingAPI.Plants.Services
{
    public class PlantLatestService(PlantLatestMapper latestMapper) : IPlantLatestService
    {
        private readonly PlantLatestMapper _latestMapper = latestMapper;

        public List<Plant> GetLatestPlants ()
        {
            return _latestMapper.GetLatestPlants();
        }
        
    }
}