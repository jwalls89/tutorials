using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Walls.Julian.Mediatr
{
    public class AnotherPipelinePreProcessor : IRequestPreProcessor<PipelineRequest>
    {
        public Task Process(PipelineRequest request, CancellationToken cancellationToken)
        {
            request.Name = request.Name + " AnotherPipelinePreProcessor";
            return Task.CompletedTask;
        }
    }
}
