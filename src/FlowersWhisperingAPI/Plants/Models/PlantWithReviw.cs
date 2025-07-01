namespace FlowersWhisperingAPI.Plants.Models
{
    public class PlantWithReview(int plantId, int reviewId, string commonName, 
        string scientificName, string modifiedContent, string reviewStatus)
    {
        public int PlantId { get; set; } = plantId;// 植物ID，唯一标识
        public int ReviewId { get; set; } = reviewId; // 评论ID，唯一标识
        public string CommonName { get; set; } = commonName; // 常见名称
        public string ScientificName { get; set; } = scientificName; // 学名
        public string modifiedContent { get; set; } = modifiedContent; // 生长环境
        public string reviewStatus { get; set; } = reviewStatus; // 养护条件
       
    }
}
