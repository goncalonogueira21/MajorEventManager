using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEM2.Config
{
    public class ToastInstance
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public ToastSettings ToastSettings { get; set; }
    }
}
