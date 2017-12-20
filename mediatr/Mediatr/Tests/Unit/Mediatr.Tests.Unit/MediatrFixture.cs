using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using MediatR;
using NUnit.Framework;
using System.Threading.Tasks;
using Walls.Julian.Mediatr;
using Walls.Julian.Mediatr.Plumbing;

namespace Mediatr.Tests.Unit
{
    [TestFixture]
    internal class MediatrFixture
    {
        private IWindsorContainer _windsorContainer;

        [SetUp]
        public void SetUp()
        {
            _windsorContainer = WindsorContainerFactory.Create();
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer?.Dispose();
        }

        private IMediator GetSut()
        {
            return _windsorContainer.Resolve<IMediator>();
        }

        [Test]
        public async Task RequestAndResponse()
        {
            //Arrange
            var sut = GetSut();

            //Act
            var result = await sut.Send(new PingRequestResponse());

            //Assert
            Assert.That(result, Is.EqualTo("Pong"));
        }

        [Test]
        public async Task RequestOnly()
        {
            //Arrange
            var sut = GetSut();

            //Act
            await sut.Send(new DisposableRequest());

            //Assert
            var handler = _windsorContainer.Resolve<OneWayHandler>();
            Assert.That(handler.TimesCalled, Is.EqualTo(1));
        }

        [Test]
        public async Task RequestAndResponseNoCancellationTask()
        {
            //Arrange
            var sut = GetSut();

            //Act
            var result = await sut.Send(new AsyncNoCancellationRequest());

            //Assert
            Assert.That(result, Is.EqualTo("AsyncNoCancellationRequest"));
        }

        [Test]
        public async Task RequestAndResponseNotAsync()
        {
            //Arrange
            var sut = GetSut();

            //Act
            var result = await sut.Send(new NoAsyncRequest());

            //Assert
            Assert.That(result, Is.EqualTo("NoAsyncRequest"));
        }

        [Ignore("No way to prove")]
        [Test]
        public async Task TestResourcesGetDisposedByWindsor()
        {
            //Arrange
            var sut = GetSut();

            //Act
            await sut.Send(new DisposableRequest());

            //Assert
            using (_windsorContainer.BeginScope())
            {
                DisposableHandler handler = _windsorContainer.Resolve<DisposableHandler>();
                Assert.That(handler.Disposed, Is.True); //checking the act instance is disposed
            }
        }

        [Test]
        public async Task Notifications()
        {
            //Arrange
            var sut = GetSut();

            //Act
            await sut.Publish(new PublishRequest());

            //Assert
            var notifier1 = _windsorContainer.Resolve<Notifier1>();
            var notifier2 = _windsorContainer.Resolve<Notifier2>();
            Assert.Multiple(() =>
            {
                Assert.That(notifier1.TimesCalled, Is.EqualTo(1));
                Assert.That(notifier2.TimesCalled, Is.EqualTo(1));
            });

        }

        //This pipeline has been set up to use a custom pipeline behaviour
        //Use the default handling for running pre processors and post processors which is to run them in the sequence provided by the IOC container
        

        [Test]
        public async Task Pipeline()
        {
            //Arrange
            var sut = GetSut();
            var expectedResult = new PipelineResponse { Result = " Pipeline behaviour start PipelinePreProcessor AnotherPipelinePreProcessor HANDLER PipelinePostProcessor AnotherPipelinePostProcessor Pipeline behaviour end", Success = true };
                                                                  
            //Act
            var result = await sut.Send(new PipelineRequest { Name = "" });

            //Assert
            Assert.That(result.Success, Is.EqualTo(expectedResult.Success));
            Assert.That(result.Result, Is.EqualTo(expectedResult.Result));
        }
    }
}
