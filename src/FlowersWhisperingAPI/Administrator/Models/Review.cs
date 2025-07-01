namespace FlowersWhisperingAPI.Administrator.Models
{
    public class Review(int reviewId, int plantId, int submitterId, string content, string reviewStatus, DateTime submittedDate, DateTime? reviewDate)
    {
        public int ReviewId { get; set; } = reviewId;
        public string? ModifiedContent { get; set; } = content;
        public DateTime SubmittedDate { get; set; } = submittedDate;
        public int SubmitterId { get; set; } = submitterId;
        public int PlantId { get; set; } = plantId;
        public string ReviewStatus { get; set; } = reviewStatus;
        public DateTime? ReviewDate { get; set; } = reviewDate;

    }
}