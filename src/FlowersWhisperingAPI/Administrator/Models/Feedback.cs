namespace FlowersWhisperingAPI.Administrator.Models
{
    public class Feedback(int feedbackId, int userId, string feedbackContent, DateTime submittedDate)
    {
        public int FeedbackId { get; set; } = feedbackId;
        public int UserId { get; set; } = userId;
        public string FeedbackContent { get; set; } = feedbackContent;
        public DateTime SubmittedDate { get; set; } = submittedDate;
    }
}