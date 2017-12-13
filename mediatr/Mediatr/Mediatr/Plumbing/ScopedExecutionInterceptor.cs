using Castle.DynamicProxy;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace Walls.Julian.Mediatr.Plumbing
{
    public class ScopedExecutionInterceptor : IInterceptor
    {
        private readonly IWindsorContainer container;

        public ScopedExecutionInterceptor(IWindsorContainer container)
        {
            this.container = container;
        }

        public void Intercept(IInvocation invocation)
        {
            using (container.BeginScope())
            {
                invocation.Proceed();
            }
        }
    }
}
