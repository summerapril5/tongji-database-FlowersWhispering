using FlowersWhisperingAPI.Administrator.Mappers;
using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;

namespace FlowersWhisperingAPI.Administrator.Services
{
    public class AdminAnnoService(AdminAnnoMapper annoMapper) : IAdminAnnoService
    {
        private readonly AdminAnnoMapper _annoMapper = annoMapper;
        public void AddAnnouncement(AnnoDTO annoDTO)
        {
            _annoMapper.AddAnnouncement(annoDTO);
        }

        public void DeleteAnnouncement(int announcementId)
        {
            _annoMapper.DeleteAnnouncement(announcementId);
        }

        public List<Announcement> GetAllAnnouncements()
        {
            return _annoMapper.GetAllAnnouncements().OrderBy(anno =>anno.AnnouncementId).ToList();
        }
    }
}