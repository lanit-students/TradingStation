namespace DataBaseService.Interfaces
{
    public interface IMapper<BusinessModel, DbModel>
    {
        DbModel CreateMap(BusinessModel data);
        BusinessModel CreateRemap(DbModel data);
    }
}
