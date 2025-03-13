using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Carrier
    {
        public int CarrierId { get; set; }
        public string CarrierName { get; set; } = string.Empty;
        public bool CarrierIsActive { get; set; }
        public decimal CarrierPlusDesiCost { get; set; }
        public int CarrierConfigurationId { get; set; }
        public ICollection<CarrierConfiguration> CarrierConfigurations { get; set; } = new List<CarrierConfiguration>();
    }

}
