using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.BlanketsHoldingNoHeat
{
    public class OrderLogic : IOrderLogic
    {
        readonly IOrderValidator _validator;
        readonly IOrderProvider _orderProvider;
        readonly ILogger _logger;

        public OrderLogic(IOrderValidator validator, IOrderProvider orderProvider, ILogger logger)
        {
            _validator = validator;
            _orderProvider = orderProvider;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderValidationError>> PlaceOrder(Order order)
        {
            var errors = await _validator.ValidateOrder(order);

            if (errors.Any())
            {
                _logger.Log("Cannot process order");
                return errors;
            }

            await _orderProvider.SaveOrder(order);

            return errors;
        }
    }
}
