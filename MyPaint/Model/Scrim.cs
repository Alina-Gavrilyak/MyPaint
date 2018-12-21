using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    public class Scrim
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<StrokeId> StrokeIds { get; set; } = new List<StrokeId>();
    }
}
