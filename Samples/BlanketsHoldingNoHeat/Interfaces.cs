using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Samples.BlanketsHoldingNoHeat
{
    public interface IOrderLogic
    {
        Task<IEnumerable<OrderValidationError>> PlaceOrder(Order order);
    }

    public interface IOrderValidator
    {
        Task<IEnumerable<OrderValidationError>> ValidateOrder(Order order);
    }

    public class OrderValidationError
    {
        public string Message { get; set; }
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public interface IOrderProvider
    {
        Task SaveOrder(Order order);
    }

}

