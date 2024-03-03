namespace Wms.Infrastructure
{
    public class Enums
    {

        public enum RoleType
        {
            SystemAdmin,
            AdminRole,
            ManagerRole,
            UserRole
        }

        public enum OrderType
        {
            Receipt,
            Transfer,
            Outbound
        }

        public enum StatusType
        {
            Open,
            InProgress,
            Closed,
            Cancelled
        }

        public enum ItemType
        {
            Box,
            Sample
        }
    }
}
