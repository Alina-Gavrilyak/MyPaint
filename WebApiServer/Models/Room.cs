using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServer.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; } = new List<User>();
        public virtual List<Board> Boards { get; set; } = new List<Board>();

    }
}