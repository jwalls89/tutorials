using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class PipelineBehavior : IPipelineBehavior<PipelineRequest, PipelineResponse>
    { 
        public async Task<PipelineResponse> Handle(PipelineRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<PipelineResponse> next)
        {
            request.Name = request.Name + " Pipeline behaviour start";
            var response = await next();
            response.Result = response.Result + " Pipeline behaviour end";
            return response;
        }
    }
}
