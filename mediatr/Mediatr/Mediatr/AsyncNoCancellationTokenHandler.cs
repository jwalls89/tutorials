using MediatR;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class AsyncNoCancellationTokenHandler : AsyncRequestHandler<AsyncNoCancellationRequest, string>
    { 
        protected override Task<string> HandleCore(AsyncNoCancellationRequest request)
        {
            return Task.FromResult("AsyncNoCancellationRequest");
        }
    }
}
