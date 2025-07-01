using FlowersWhisperingAPI.Administrator.Models;

namespace FlowersWhisperingAPI.Administrator.Services.Interface
{
    public interface IAdminAnnoService
    {
        public List<Announcement> GetAllAnnouncements();
        public void AddAnnouncement(AnnoDTO annoDTO);
        public void DeleteAnnouncement(int announcementId);
    }
}