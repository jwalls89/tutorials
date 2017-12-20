using MediatR.Pipeline;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class AnotherPipelinePostProcessor : IRequestPostProcessor<PipelineRequest, PipelineResponse>
    {
        public Task Process(PipelineRequest request, PipelineResponse response)
        {
            response.Result = response.Result + " AnotherPipelinePostProcessor";
            return Task.CompletedTask;
        }
    }
}
