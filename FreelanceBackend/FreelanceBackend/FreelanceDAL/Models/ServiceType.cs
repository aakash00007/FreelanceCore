using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Models
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string ServiceName { get; set; }
        public virtual ICollection<Service>? Service { get; set; }
    }
}
