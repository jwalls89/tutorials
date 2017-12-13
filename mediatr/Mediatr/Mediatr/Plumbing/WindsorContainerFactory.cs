using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MediatR;
using System.Collections.Generic;

namespace Walls.Julian.Mediatr.Plumbing
{
    public static class WindsorContainerFactory
    {
        public static IWindsorContainer Create()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IMediator>().ImplementedBy<Mediator>().Interceptors(InterceptorReference.ForType<ScopedExecutionInterceptor>()).Last); //Looks like it can be a singleton looking at the source code as no state
            container.Register(Component.For<SingleInstanceFactory>().UsingFactoryMethod<SingleInstanceFactory>(k => k.Resolve));
            container.Register(Component.For<MultiInstanceFactory>().UsingFactoryMethod<MultiInstanceFactory>(k => t => (IEnumerable<object>)k.ResolveAll(t)));

            container.Register(Component.For<IRequestHandler<PingRequestResponse, string>>().ImplementedBy<RequestAndResponseHandler>().LifestyleSingleton());
            container.Register(Component.For<IRequestHandler<DisposableRequest>, OneWayHandler>().ImplementedBy<OneWayHandler>().LifestyleSingleton());
            container.Register(Component.For<IRequestHandler<AsyncNoCancellationRequest, string>>().ImplementedBy<AsyncNoCancellationTokenHandler>().LifestyleSingleton());
            container.Register(Component.For<IRequestHandler<NoAsyncRequest, string>>().ImplementedBy<NoAsyncHandler>().LifestyleSingleton());

            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
            container.Register(Component.For<ScopedExecutionInterceptor>().ImplementedBy<ScopedExecutionInterceptor>().LifestyleSingleton());
            container.Register(Component.For<IRequestHandler<DisposableRequest>, DisposableHandler>().Instance(new DisposableHandler()).LifestyleScoped());

            container.Register(Component.For<INotificationHandler<PublishRequest>, Notifier1>().ImplementedBy<Notifier1>().LifestyleSingleton());
            container.Register(Component.For<INotificationHandler<PublishRequest>, Notifier2>().ImplementedBy<Notifier2>().LifestyleSingleton());

            return container;
        }
    }
}
