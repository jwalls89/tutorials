using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class OneWayHandler : IRequestHandler<DisposableRequest>
    {
        public int TimesCalled { get; private set; }

        public Task Handle(DisposableRequest request, CancellationToken cancellationToken)
        {
            TimesCalled++;
            return Task.CompletedTask;
        }
    }
}
