using FlowersWhisperingAPI.Administrator.Mappers;
using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;

namespace FlowersWhisperingAPI.Administrator.Services
{
    public class AdminReviewService(AdminReviewMapper reviewMapper) : IAdminReviewService
    {        
        private readonly AdminReviewMapper _reviewMapper = reviewMapper;
        public List<Review> GetAllReviews()
        {
            List<Review> reviews = _reviewMapper.GetAllReviews();
            List<Review> sortedReviews = reviews.OrderBy(review => review.ReviewId).ToList();
            return sortedReviews;
        }

        public void Pass(int reviewId)
        {
            _reviewMapper.UpdateReviewStatus(reviewId,"审核通过");
        }

        public void NotPass(int reviewId)
        {
            _reviewMapper.UpdateReviewStatus(reviewId,"不通过");
        }

        public Models.Plant? SelectPlantById(int plantId)
        {
            Models.Plant? plant = _reviewMapper.SelectPlantById(plantId);
            string url = _reviewMapper.GetImageUrl(plantId);
            if(plant != null && url != null) 
                plant.ImageUrl = url;
            return plant;
        }

        public void AddPlant(Models.Plant plant)
        {
            _reviewMapper.AddPlant(plant.CommonName, plant.ScientificName, plant.Category, plant.Portrayal, plant.GrowthEnvironment, plant.CareConditions);
            int plantId = _reviewMapper.GetPlantId(plant.ScientificName);
            if(plant.ImageUrl != null)
                _reviewMapper.AddImageUrl(plantId,plant.ImageUrl);
        }

        public void UpdatePlant(Models.Plant plant)
        {
            _reviewMapper.UpdatePlant(plant.PlantId, plant.CommonName, plant.ScientificName, plant.Category, plant.Portrayal, plant.GrowthEnvironment, plant.CareConditions);
            int plantId = _reviewMapper.GetPlantId(plant.ScientificName);
            if(plant.ImageUrl != null)
                _reviewMapper.UpdateImageUrl(plantId,plant.ImageUrl);
        }

        public void DeletePlant(int plantId)
        {
            _reviewMapper.DeleteImageUrl(plantId);
            _reviewMapper.DeletePlant(plantId);  
        }
    }
}