using System;
using System.Collections.Generic;
using System.Text;

namespace ShipContentManager.Models
{
    public class Pack
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsMiniPack { get; set; }
        public string PackObjectId { get; set; }


        public string DateCreatedToString()
        {
            return DateCreated.ToString("dd/MM/yyyy");
        }
    }
}
