using FlowersWhisperingAPI.Plants.Mappers;
using FlowersWhisperingAPI.Plants.Models;
using FlowersWhisperingAPI.Plants.Services.Interface;

namespace FlowersWhisperingAPI.Plants.Services
{
    public class PlantFindService(PlantFindMapper findMapper) : IPlantFindService
    {
        private readonly PlantFindMapper _findMapper = findMapper;

        public int GetPlantId (string plantName)
        {
            return _findMapper.GetPlantId(plantName);
        }
        public Plant GetPlantInfo (string plantName)
        {
            return _findMapper.GetPlantInfo(plantName);
        }
        
    }
}