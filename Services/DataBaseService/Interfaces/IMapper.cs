namespace DataBaseService.Interfaces
{
    public interface IMapper<B, Db>
    {
        Db CreateMap(B data);
        B CreateRemap(Db data);
    }
}
