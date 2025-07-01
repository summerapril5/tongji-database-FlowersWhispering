using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlowersWhisperingAPI.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyShortNames(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // 缩短表名
                var tableName = entityType.GetTableName()?? "DefaultTableName";
                if (tableName != null && tableName.Length > 27)
                {
                    entityType.SetTableName(tableName.Substring(0, 27));
                }
            }
        }
    }
}
