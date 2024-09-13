using PathFinder.Core.Entities;
using PathFinder.Infrastructure.DBContexts;
using Microsoft.AspNetCore.SignalR;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class NotificationHub :  Hub
    {
        private readonly PathFinderDBContext _dbContext;
        public NotificationHub(PathFinderDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string reciverName)
        {
            var hubConnections = _dbContext.HubConnection.Where(con => con.ReciverName == reciverName).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, reciverName);
            }
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string reciverName)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                HubConnection hubConnection = new HubConnection
                {
                    ConnectionId = connectionId,
                    ReciverName = string.IsNullOrWhiteSpace(reciverName) ? "Admin" : reciverName,
                };

                _dbContext.HubConnection.Add(hubConnection);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var hubConnection = _dbContext.HubConnection.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
                if (hubConnection != null)
                {
                    _dbContext.HubConnection.Remove(hubConnection);
                    _dbContext.SaveChangesAsync();
                }

                return base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
