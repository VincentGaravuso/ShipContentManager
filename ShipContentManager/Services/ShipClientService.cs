using Shared_ShipContentManager.Interfaces;
using Shared_ShipContentManager.Services;

namespace ShipContentManager.Services
{
    public class ShipClientService
    {
        public IShipClientService ShipService { get; set; } = new ShipClient();
    }
}
