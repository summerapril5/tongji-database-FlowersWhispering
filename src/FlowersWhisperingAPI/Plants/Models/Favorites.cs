namespace FlowersWhisperingAPI.Plants.Models
{
    public class Favorite(int favoriteId, int userId, int plantId)
    {
        // 主键，使用自动生成的标识字段
        public int FavoriteId { get; set; } = favoriteId;
        // 外键，指向 Users 表
        public int UserId { get; set; } = userId;
        // 外键，指向 Plants 表
        public int PlantId { get; set; } = plantId;
    }

}
