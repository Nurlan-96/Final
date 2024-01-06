using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Domain.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
