using FlowersWhisperingAPI.Community.Mappers;
using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;


namespace FlowersWhisperingAPI.Community.Services
{
    public class CommuniFavorService(CommuniFavorMapper favorMapper) : ICommuniFavorService
    {
        private readonly CommuniFavorMapper _favorMapper = favorMapper;
        public void AddFavor(FavorDTO favordto)
        {
            _favorMapper.AddFavor(favordto);
        }
        public void DeleteFavor(int favorId)
        {
            _favorMapper.DeleteFavor(favorId);
        }

        public List<Favor> GetAllFavor(int userId)
        {
            return _favorMapper.GetAllFavor(userId);
        }

    }
}