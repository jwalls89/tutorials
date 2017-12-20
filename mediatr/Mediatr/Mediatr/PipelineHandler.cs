using MediatR;

namespace Walls.Julian.Mediatr
{
    public class PipelineHandler : RequestHandler<PipelineRequest, PipelineResponse>
    {
        protected override PipelineResponse HandleCore(PipelineRequest request)
        {
            PipelineResponse pipelineResponse = new PipelineResponse {
                Result = request.Name + " HANDLER",
                Success = true
            };
            return pipelineResponse;
        }
    }
}
