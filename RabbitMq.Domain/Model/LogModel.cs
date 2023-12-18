using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Domain.Model
{
    public class LogModel
    {
        public string ServiceName { get; set; }
        public string Url { get; set; }
        public int StatusCode { get; set; }
    }
}
