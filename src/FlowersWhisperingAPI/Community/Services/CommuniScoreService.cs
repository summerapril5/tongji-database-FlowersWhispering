using FlowersWhisperingAPI.Community.Mappers;
using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;

namespace FlowersWhisperingAPI.Community.Services
{
    public class CommuniScoreService(CommuniScoreMapper scoreMapper) : ICommuniScoreService
    {
        private readonly CommuniScoreMapper _scoreMapper = scoreMapper;
        public int GetUserScore(int userId)
        {
            return _scoreMapper.GetUserScore(userId);
        }
        public List<Score> RankScores()
        {
            return _scoreMapper.RankScores();
        }

    }
}