using FlowersWhisperingAPI.Administrator.Models;

namespace FlowersWhisperingAPI.Administrator.Services.Interface
{
    public interface IAdminReviewService
    {
        public List<Review> GetAllReviews();
        public void Pass(int reviewId);
        public void NotPass(int reviewId);
        public Models.Plant? SelectPlantById(int plantId);
        public void AddPlant(Models.Plant plant);
        public void UpdatePlant(Models.Plant plant);
        public void DeletePlant(int plantId);
    }
}