using MediatR.Pipeline;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class PipelinePostProcessor : IRequestPostProcessor<PipelineRequest, PipelineResponse>
    {
        public Task Process(PipelineRequest request, PipelineResponse response)
        {
            response.Result = response.Result + " PipelinePostProcessor";
            return Task.CompletedTask;
        }
    }
}
