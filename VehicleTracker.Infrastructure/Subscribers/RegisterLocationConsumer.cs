using MassTransit;
using MediatR;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Vehicle.Command;
using VehicleTracker.Common.Messages;

namespace VehicleTracker.Infrastructure.Subscribers
{
    public class RegisterLocationConsumer : IConsumer<RegisterLocationConsumerMessage>
    {
        private readonly ISender _sender;
        public RegisterLocationConsumer(ISender sender)
        {
            _sender = sender;
        }
        public async Task Consume(ConsumeContext<RegisterLocationConsumerMessage> context)
        {
            //Receive event from a vehicle publisher from another domain
            await _sender.Send(new AddVehicleLocationCommand()
            {
                UserId = "",
                Latitude = context.Message.Latitude,
                Longitude = context.Message.Longitude
            });
        }
    }
}
