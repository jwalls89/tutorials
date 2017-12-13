using MediatR;

namespace Walls.Julian.Mediatr
{
    public class NoAsyncHandler : RequestHandler<NoAsyncRequest, string>
    {
        protected override string HandleCore(NoAsyncRequest request)
        {
            return "NoAsyncRequest";
        }
    }
}
