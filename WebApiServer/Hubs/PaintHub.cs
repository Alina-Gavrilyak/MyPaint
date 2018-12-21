using ClientServerClassLibrary;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiServer.Models;

namespace WebApiServer.Hubs
{
    [Authorize]
    public class PaintHub : Hub
    {
        static Dictionary<string, string> userConnectionDictionary = new Dictionary<string, string>();

        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PaintHub>();

        public static void AddStroke(List<string> userIds, StrokeData strokeData)
        {
            foreach (string id in userIds)
                if (userConnectionDictionary.ContainsKey(id))
                    hubContext.Clients.Client(userConnectionDictionary[id]).AddStroke(strokeData);
        }
        public static void RemoveStroke(List<string> userIds, Guid strokeId)
        {
            foreach (string id in userIds)
                if (userConnectionDictionary.ContainsKey(id))
                    hubContext.Clients.Client(userConnectionDictionary[id]).RemoveStroke(id);
        }


        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();

            if (userId == null)
                Clients.Client(Context.ConnectionId).serverOrderedDisconnect();

            if (userConnectionDictionary.ContainsKey(userId))
                userConnectionDictionary[userId] = Context.ConnectionId;
            else
                userConnectionDictionary.Add(userId, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            string userId = Context.User.Identity.GetUserId();

            if (userConnectionDictionary.ContainsKey(userId))
                userConnectionDictionary[userId] = Context.ConnectionId;
            else
                userConnectionDictionary.Add(userId, Context.ConnectionId);


            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = Context.User.Identity.GetUserId();

            if (userId == null)
                return null;

            Clients.Client(Context.ConnectionId).serverOrderedDisconnect();

            if (userConnectionDictionary.ContainsValue(Context.ConnectionId))
                userConnectionDictionary.Remove(userId);

            return base.OnDisconnected(stopCalled);
        }
    }
}