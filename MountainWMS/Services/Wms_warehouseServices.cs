using IRepository;
using IServices;
using SqlSugar;
using System;
using M.Core.Entity;
using M.Utils.Extensions;
using M.Utils.Json;
using M.Utils.Table;

namespace Services
{
    public class Wms_warehouseServices : BaseServices<Wms_warehouse>, IWms_warehouseServices
    {
        private readonly IWms_warehouseRepository _repository;
        private readonly SqlSugarClient _client;

        public Wms_warehouseServices(SqlSugarClient client, IWms_warehouseRepository repository) : base(repository)
        {
            _repository = repository;
            _client = client;
        }

        public string PageList(Bootstrap.BootstrapParams bootstrap)
        {
            int totalNumber = 0;
            if (bootstrap.offset != 0)
            {
                bootstrap.offset = bootstrap.offset / bootstrap.limit + 1;
            }
            var query = _client.Queryable<Wms_warehouse, Sys_user, Sys_user>
                ((s, c, u) => new object[] {
                   JoinType.Left,s.CreateBy==c.UserId,
                   JoinType.Left,s.ModifiedBy==u.UserId
                 })
                 .Select((s, c, u) => new
                 {
                     WarehouseId = s.WarehouseId.ToString(),
                     s.WarehouseNo,
                     s.WarehouseName,
                     s.IsDel,
                     s.Remark,
                     CName = c.UserNickname,
                     s.CreateDate,
                     UName = u.UserNickname,
                     s.ModifiedDate
                 }).MergeTable().Where((s) => s.IsDel == 1);
            if (!bootstrap.search.IsEmpty())
            {
                query.Where((s) => s.WarehouseName.Contains(bootstrap.search) || s.WarehouseNo.Contains(bootstrap.search));
            }
            if (!bootstrap.datemin.IsEmpty() && !bootstrap.datemax.IsEmpty())
            {
                query.Where(s => s.CreateDate > bootstrap.datemin.ToDateTimeB() && s.CreateDate <= bootstrap.datemax.ToDateTimeE());
            }
            if (bootstrap.order.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                query.OrderBy($"MergeTable.{bootstrap.sort} desc");
            }
            else
            {
                query.OrderBy($"MergeTable.{bootstrap.sort} asc");
            }
            var list = query.ToPageList(bootstrap.offset, bootstrap.limit, ref totalNumber);
            return Bootstrap.GridData(list, totalNumber).JilToJson();
        }
    }
}