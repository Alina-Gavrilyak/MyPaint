using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServer.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Room Room { get; set; }
        public Guid RoomId { get; set; }
        public List<Stroke> Strokes{ get; set; }
    }
}
