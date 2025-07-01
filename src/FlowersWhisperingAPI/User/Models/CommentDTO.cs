namespace FlowersWhisperingAPI.User.Models
{
    public class CommentDTO
    {
        public int UserId { get; set; } = 0;
        public int PlantId { get; set; } = 0;
        public string CommentContent { get; set; } = null!;       

    }   
}