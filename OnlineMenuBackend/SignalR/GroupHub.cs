

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineMenu.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineMenu.SignalR
{
    public class GroupHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var token = Context.GetHttpContext().Request.Query["access_token"];
            var tokens = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token);
            var tesst = tokens.Claims.ToList();
            var tenantid = tesst[3].Value;
            AddToGroup(Context.ConnectionId, tenantid);
            return base.OnConnectedAsync();
        }

        public async Task AddToGroup(string ConnectionId,string groupName)
        {
            await Groups.AddToGroupAsync(ConnectionId, groupName);
        }
        public async Task SendOrdertoGroup(string groupName, Order order)
        {
            await Clients.Group(groupName).SendAsync("You recieved an order", order);
        }
    }
}

