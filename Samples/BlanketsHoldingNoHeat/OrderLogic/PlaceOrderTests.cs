//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Samples.BlanketsHoldingNoHeat.OrderLogicTests
//{

//    public class PlaceOrderTests
//    {
//        [Fact]
//        public async Task WhenValidationErrorsExist_ReturnsErrorsAndDoesNotSave()
//        {
//            //arrange
//            Mock<IOrderValidator> mockValidator = new Mock<IOrderValidator>();
//            Mock<IOrderProvider> mockProvider = new Mock<IOrderProvider>();
//            Mock<ILogger> mockLogger = new Mock<ILogger>();

//            mockValidator.Setup(v => v.ValidateOrder(It.IsAny<Order>()))
//                .ReturnsAsync(new OrderValidationError[] {
//        new OrderValidationError()
//        {
//            Message = "Live long and prosper"
//        }
//                });
//            var logicUnderTest = new OrderLogic(
//                mockValidator.Object,
//                mockProvider.Object,
//                mockLogger.Object);

//            //act
//            var result = await logicUnderTest.PlaceOrder(new Order());

//            //assert
//            mockLogger.Verify(l => l.Log(It.IsAny<string>()));
//            Assert.Single(result);
//            var error = result.First();
//            Assert.Equal("Live long and prosper", error.Message);

//            mockProvider.Verify(p => p.SaveOrder(It.IsAny<Order>()), Times.Never);
//        }
//    }
//}
