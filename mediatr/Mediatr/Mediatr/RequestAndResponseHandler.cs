using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class RequestAndResponseHandler : IRequestHandler<PingRequestResponse, string>
    {
        public Task<string> Handle(PingRequestResponse request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
