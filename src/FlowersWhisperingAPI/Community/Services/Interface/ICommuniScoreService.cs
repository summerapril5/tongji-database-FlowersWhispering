using FlowersWhisperingAPI.Community.Models;

namespace FlowersWhisperingAPI.Community.Services.Interface
{

    public interface ICommuniScoreService
    {
        public List<Score> RankScores();

        public int GetUserScore(int userId);

    }
}

