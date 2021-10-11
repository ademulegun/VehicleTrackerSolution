using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.Core.Entities;

namespace VehicleTracker.Infrastructure.DomainEvents.Hub
{
    [Authorize]
    public class VehicleTrackerHub: Hub<ITypedHubClient>
    {
        private readonly ILogger<VehicleTrackerHub> _logger;
        private readonly IVehicleTrackerDbContext _db;
        private readonly IDictionary<string, string> _connections;
        private readonly IIdentityService _identityService;
        public VehicleTrackerHub(IVehicleTrackerDbContext db, IDictionary<string, string> connections, ILogger<VehicleTrackerHub> logger, IIdentityService identityService)
        {
            _db = db;
            _connections = connections;
            _logger = logger;
            _identityService = identityService;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation("Client with conectionid: {@Connectionid} just connected", connectionId);
            return base.OnConnectedAsync();
        }

        public async Task JoinGroup(Guid groupName)
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation("{@Group} entered the join group method and connectionId is: {@ConnectionId}", groupName, connectionId);
            try
            {
                _logger.LogInformation("{@GroupName} is about joining: {@Group}", groupName, groupName);
                if (_connections.TryAdd(connectionId, groupName.ToString()))
                {
                    _logger.LogInformation("Customer with connection id: {@Connectionid}, joined group: {@Group} successfully", connectionId, groupName);
                    await Groups.AddToGroupAsync(connectionId, groupName.ToString());
                    _connections[connectionId] = groupName.ToString();
                    await Clients.Group(groupName.ToString()).JoinedRoomConfirmation($"{groupName} has just joined");
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while joining {@Group}", groupName);
                throw;
            }
        }

        private string GetGroupName(string connectionId)
        {
            var result = _connections.Where(x => x.Key == connectionId).Select(x => x).FirstOrDefault();
            return result.Value;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var groupName = GetGroupName(connectionId);
            if (_connections.TryGetValue(connectionId, out var roomName))
            {
                _logger.LogInformation("Removing user with connectionId of: {@ConnectionId} from group with name of: {@groupName}", connectionId, groupName);
                _connections.Remove(connectionId);
                _logger.LogInformation("Removed user with connectionId of: {@ConnectionId} from group with name of: {@groupName}", connectionId, groupName);
            }
            await base.OnDisconnectedAsync(exception);
            _logger.LogInformation("Customer with connection id: {@Connection} has just disconnected", connectionId);
        }
    }
}
