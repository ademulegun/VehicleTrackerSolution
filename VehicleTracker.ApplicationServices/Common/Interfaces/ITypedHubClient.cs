using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ApplicationServices.Common.Interfaces
{
    public interface ITypedHubClient
    {
        Task JoinedRoomConfirmation(string message);
        Task PublishLocation(string longitude, string latitude);
    }
}
