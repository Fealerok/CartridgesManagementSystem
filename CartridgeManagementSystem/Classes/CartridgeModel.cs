using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartridgeManagementSystem.Classes
{
    public class CartridgeModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public long SerialNumber { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
