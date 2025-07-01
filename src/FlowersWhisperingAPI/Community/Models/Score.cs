namespace FlowersWhisperingAPI.Community.Models{

    public class Score(int userId, int totalScore)
    {
        public int UserId { get; set; } = userId;
        public int TotalScore { get; set; } = totalScore;
        public string Username { get; set; } = null!;
    }

}