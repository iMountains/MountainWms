using IRepository;
using SqlSugar;
using M.Core.Entity;

namespace Repository
{
    public class Sys_serialnumRepository : BaseRepository<Sys_serialnum>, ISys_serialnumRepository
    {
        public Sys_serialnumRepository(SqlSugarClient dbContext) : base(dbContext)
        {
        }
    }
}