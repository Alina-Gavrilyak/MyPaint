using WebApiServer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClientServerClassLibrary;
using WebApiServer.Hubs;

namespace WebApiServer.Controllers
{
    [Authorize]
    public class PaintController : ApiController
    {
        readonly PaintContext db = new PaintContext();

        [AllowAnonymous]
        public void SignUp([FromBody] RegistrationData data)
        {
            if (String.IsNullOrEmpty(data.UserName) || String.IsNullOrEmpty(data.UserName))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(db));

            User user = new User() { UserName = data.UserName };

            IdentityResult result = userManager.CreateAsync(user, data.Password).Result;

            if (!result.Succeeded)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public IEnumerable<object> GetUsers()
        {
            string userId = User.Identity.GetUserId();
            return db.Users.Where(u => u.Id != userId).Select(u => new
            {
                u.Id,
                Name = u.UserName
            });
        }

        public IEnumerable<object> GetRooms()
        {
            string userId = User.Identity.GetUserId();
            return db.Users.Where(u => u.Id == userId).SelectMany(u => u.Rooms).Select(r => new
            {
                r.Id,
                r.Name,
                Scrims = r.Boards.Select(x => new { x.Id, x.Name })
            });
        }

        public Guid AddBoard(AddBoardData data)
        {
            Board board = new Board
            {
                Id = Guid.NewGuid(),
                Name = data.BoardName,
                RoomId = data.RoomId,
            };
            db.Boards.Add(board);
            db.SaveChanges();
            return board.Id;
        }

        public Guid AddRoom(AddRoomData data)
        {
            string userId = User.Identity.GetUserId();
            Room r = new Room()
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
            };
            foreach (Guid id in data.UsersId)
            {
                r.Users.Add(db.Users.First(u => u.Id == id.ToString()));
            }
            r.Users.Add(db.Users.First(u => u.Id == userId));

            db.Rooms.Add(r);
            db.SaveChanges();
            return r.Id;
        }

        [HttpPost]
        public List<StrokeData> GetStrokes([FromBody] Guid boardId)
        {
            return db.Strokes.Where(s => s.BoardId == boardId).Select(s => new StrokeData
            {
                Id = s.Id,
                Data = s.Data
            }).ToList();
        }

        public Guid AddStroke(AddStrokeData data)
        {
            Stroke s = new Stroke()
            {
                Id = Guid.NewGuid(),
                BoardId = data.BoardId,
                Data = data.Data
            };
            db.Strokes.Add(s);
            db.SaveChanges();
            List<string> userIds = db.Boards.First(b => b.Id == data.BoardId).Room.Users.Select(u => u.Id).ToList();
            PaintHub.AddStroke(userIds, new StrokeData() { Id = s.Id, Data = data.Data, BoardId = data.BoardId });
            return s.Id;
        }

        [HttpPost]
        public void RemoveStroke([FromBody] Guid id)
        {
            Stroke stroke = db.Strokes.FirstOrDefault(s => s.Id == id);
            if (stroke == null)
                return;
            List<string> userIds = stroke.Board.Room.Users.Select(u => u.Id).ToList();
            db.Strokes.Remove(stroke);
            db.SaveChanges();
            PaintHub.RemoveStroke(userIds, id);
        }
    }
}
