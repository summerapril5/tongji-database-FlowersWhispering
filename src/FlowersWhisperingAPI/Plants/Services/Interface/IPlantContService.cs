using FlowersWhisperingAPI.Plants.Models;

namespace FlowersWhisperingAPI.Plants.Services.Interface
{
    public interface IPlantContService
    {
        //
        public List<PlantWithReview> GetContPlants (int userId);

        public bool AddContPlant (int userId, string plantName, string reviewContent);
    }

}