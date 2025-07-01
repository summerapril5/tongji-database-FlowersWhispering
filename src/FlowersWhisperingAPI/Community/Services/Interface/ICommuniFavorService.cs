using FlowersWhisperingAPI.Community.Models;

namespace FlowersWhisperingAPI.Community.Services.Interface
{
    public interface ICommuniFavorService
    {
        public void AddFavor(FavorDTO favordto);
        public void DeleteFavor(int favorId);

        public List<Favor> GetAllFavor(int userId);
    }
}



