using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServer.Models
{
    public class User : IdentityUser
    {
        public virtual List<Room> Rooms { get; set; } = new List<Room>();
        public virtual List<Image> Images { get; set; } = new List<Image>();
    }
}
