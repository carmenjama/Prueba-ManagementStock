namespace ManagementProducts.Master.Interfaces
{
    public interface ICrudManagement
    {
        void SetPaginated(bool isPaginated, int page, int limit);
    }
}
