namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface IProvinceCityRepository :IGenericRepository<ProvinceCity>
    {
        Task<bool> AddUpdate(ProvinceCity provinceCity);
    }
}
