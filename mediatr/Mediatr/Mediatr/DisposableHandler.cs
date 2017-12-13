using System;
using MediatR;

namespace Walls.Julian.Mediatr
{
    public class DisposableHandler : RequestHandler<DisposableRequest>, IDisposable
    {
        public bool Disposed { get; private set; }

        protected override void HandleCore(DisposableRequest message)
        {
            //Do Nothing
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed");
            Disposed = true;
        }

        
    }
}
