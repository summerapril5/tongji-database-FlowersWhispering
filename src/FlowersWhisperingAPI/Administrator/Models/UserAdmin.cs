namespace FlowersWhisperingAPI.Administrator.Models
{
    public class UserAdmin(int userId, string username, string uemail, string userstatus, string userrole)
    {
        public int UserId { get; set; } = userId;
        public string UserName { get; set; } = username;
        public string Uemail { get; set; } = uemail;
        public string Userstatus { get; set; } = userstatus;
        public string Userrole { get; set; } = userrole;
    }
}