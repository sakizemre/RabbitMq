using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq
{
    public interface IUrlProcessor
    {
        Task ProcessUrl(string url);
    }
}
