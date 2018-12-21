using System;
using System.Collections.Generic;
using System.Text;

namespace ClientServerClassLibrary
{
    public class AddStrokeData
    {
        public Guid BoardId { get; set; }
        public byte[] Data { get; set; }
    }
}
