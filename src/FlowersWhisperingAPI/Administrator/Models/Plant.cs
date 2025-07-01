namespace FlowersWhisperingAPI.Administrator.Models
{
    public class Plant
    {
        public int PlantId { get; set; }
        public string CommonName { get; set; }
        public string ScientificName { get; set; }
        public string Category { get; set; }
        public string Portrayal { get; set; }
        public string GrowthEnvironment { get; set; }
        public string CareConditions { get; set; }
        public string? ImageUrl { get; set; }

        public Plant(int plantId, string commonName, string scientificName, string category, 
                     string portrayal, string growthEnvironment, string careConditions, 
                     string? imageUrl = null)
        {
            PlantId = plantId;
            CommonName = commonName;
            ScientificName = scientificName;
            Category = category;
            Portrayal = portrayal;
            GrowthEnvironment = growthEnvironment;
            CareConditions = careConditions;
            ImageUrl = imageUrl;
        }
    }
}