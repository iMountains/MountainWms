using M.Core.Entity;
using M.Utils.Table;

namespace IServices
{
    public interface IWms_deliveryServices : IBaseServices<Wms_delivery>
    {
        string PageList(Bootstrap.BootstrapParams bootstrap);

        bool Delivery(Wms_delivery delivery);
    }
}