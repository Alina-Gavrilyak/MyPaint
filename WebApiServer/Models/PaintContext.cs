using WebApiServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApiServer.Models
{
    public class PaintContext: IdentityDbContext<User>
    {
        public PaintContext(): base("PaintDb")
        {

        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Stroke> Strokes { get; set; }

    }
}
