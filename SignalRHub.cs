using Microsoft.AspNetCore.SignalR;

public class SignalRHub : Hub<IClient>
{
  public async Task Join(string groupId) {
    await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
  }

  public void Share(string groupId, dynamic state)
  {
    Clients.OthersInGroup(groupId).Update(state);
  }
}

public interface IClient
{
  Task Update(dynamic state);
}
