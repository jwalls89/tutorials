using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Walls.Julian.Mediatr
{
    public class PipelinePreProcessor : IRequestPreProcessor<PipelineRequest>
    {
        public Task Process(PipelineRequest request, CancellationToken cancellationToken)
        {
            request.Name = request.Name + " PipelinePreProcessor";
            return Task.CompletedTask;
        }
    }
}
