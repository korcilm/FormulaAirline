using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaAirline.API.Services
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T message);
    }
}