using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Walls.Julian.Mediatr
{
    public class Notifier2 : INotificationHandler<PublishRequest>
    {
        public int TimesCalled { get; set; }

        public async Task Handle(PublishRequest notification, CancellationToken cancellationToken)
        {
            TimesCalled++;
        }
    }
}
