using System;
using System.Collections.Generic;
using System.Text;

namespace ClientServerClassLibrary
{
    public class StrokeData
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public Guid BoardId { get; set; }
    }
}
