using Castle.Windsor;
using MediatR;
using NUnit.Framework;
using Walls.Julian.Mediatr.Plumbing;

namespace Mediatr.Tests.Unit.Plumbing
{
    [TestFixture]
    internal class WindsorContainerFactoryFixture
    {
        private IWindsorContainer _containerToDisposeAfterTest;

        [TearDown]
        public void TearDown()
        {
            _containerToDisposeAfterTest?.Dispose();
        }

        private IWindsorContainer GetSut()
        {
            _containerToDisposeAfterTest = WindsorContainerFactory.Create();
            return _containerToDisposeAfterTest;
        }

        [Test]
        public void CanResolveMediator()
        {
            //Arrange
            var sut = GetSut();

            //Act
            var mediator = sut.Resolve<IMediator>();

            //Assert
            Assert.That(mediator, Is.Not.Null);
        }

    }
}
