using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQAbstraction.Handlers
{
    public interface ITopicHandler
    {
        bool Handle(string message, string correlationId);
    }
}
