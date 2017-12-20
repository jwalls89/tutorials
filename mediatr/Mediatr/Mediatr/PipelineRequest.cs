using MediatR;

namespace Walls.Julian.Mediatr
{
    public class PipelineRequest : IRequest<PipelineResponse>
    {
        public string Name { get; set; }
    }
}
