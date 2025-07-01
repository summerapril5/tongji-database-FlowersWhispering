namespace FlowersWhisperingAPI.Plants.Models
{
    public class Plant(int plantId, string commonName, 
        string scientificName, string category, string portrayal, 
        string growthEnvironment, string careConditions, DateTime UpdateTime)
    {
        public int PlantId { get; set; } = plantId;// 植物ID，唯一标识
        public string CommonName { get; set; } = commonName; // 常见名称
        public string ScientificName { get; set; } = scientificName; // 学名
        public string Category { get; set; } = category; // 分类
        public string Portrayal { get; set; } = portrayal; // 描述
        public string GrowthEnvironment { get; set; } = growthEnvironment; // 生长环境
        public string CareConditions { get; set; } = careConditions; // 养护条件
        public DateTime UpdateTime { get; set; } = UpdateTime;

        public string ImageUrl { get; set; } = ""; // 图片地址
    }
}
