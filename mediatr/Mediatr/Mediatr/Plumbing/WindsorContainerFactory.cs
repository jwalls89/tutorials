using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using MediatR;
using MediatR.Pipeline;
using System.Collections.Generic;

namespace Walls.Julian.Mediatr.Plumbing
{
    public static class WindsorContainerFactory
    {
        public static IWindsorContainer Create()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

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

            //This is just a specific example and has been heavily constrained
            container.Register(Component.For<IRequestHandler<PipelineRequest, PipelineResponse>>().ImplementedBy<PipelineHandler>().LifestyleSingleton());
            container.Register(Component.For<IRequestPreProcessor<PipelineRequest>>().ImplementedBy<PipelinePreProcessor>());
            container.Register(Component.For<IRequestPostProcessor<PipelineRequest, PipelineResponse>>().ImplementedBy<PipelinePostProcessor>());
            container.Register(Component.For<IRequestPreProcessor<PipelineRequest>>().ImplementedBy<AnotherPipelinePreProcessor>());
            container.Register(Component.For<IRequestPostProcessor<PipelineRequest, PipelineResponse>>().ImplementedBy<AnotherPipelinePostProcessor>());
            container.Register(Component.For(typeof(IPipelineBehavior<PipelineRequest, PipelineResponse>)).ImplementedBy<PipelineBehavior>());


            container.Register(Component.For(typeof(IPipelineBehavior<PipelineRequest,PipelineResponse>)).ImplementedBy(typeof(RequestPreProcessorBehavior<PipelineRequest,PipelineResponse>)).NamedAutomatically("PreProcessorBehavior"));
            container.Register(Component.For(typeof(IPipelineBehavior<PipelineRequest,PipelineResponse>)).ImplementedBy(typeof(RequestPostProcessorBehavior<PipelineRequest,PipelineResponse>)).NamedAutomatically("PostProcessorBehavior"));




            return container;
        }
    }
}
