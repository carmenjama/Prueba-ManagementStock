using ManagementProducts.Master.Interfaces.TableRow;
using ManagementProducts.SharedDto;
using ManagementProducts.SharedDto.Dto.Enums;

namespace ManagementProducts.Use.Cases.Shared
{
    public static class AuditExtension
    {
        public static TEntity InsertAudit<TEntity>(this TEntity entity, string userName) where TEntity : IAuditTableRow
        {
            entity.Status = TypeState.Active.GetEnumDescription();
            entity.CreatedBy = string.IsNullOrEmpty(userName) ? Environment.MachineName : userName;
            entity.CreatedHost = Environment.MachineName;
            entity.CreatedDate = DateTime.Now;
            return entity;
        }

        public static TEntity UpdateAudit<TEntity>(this TEntity entity, string userName, string status = null) where TEntity : IAuditTableRow
        {
            if (!string.IsNullOrEmpty(status)) entity.Status = TypeState.Active.GetEnumDescription();
            if (string.IsNullOrEmpty(entity.Status)) entity.Status = TypeState.Active.GetEnumDescription();
            entity.ModifiedBy = string.IsNullOrEmpty(userName) ? Environment.MachineName : userName;
            entity.ModifiedHost = Environment.MachineName;
            entity.ModifiedDate = DateTime.Now;
            return entity;
        }

    }
}
