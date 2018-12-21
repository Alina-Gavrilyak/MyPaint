using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServer.Models
{
    public class Stroke
    {
        public Guid Id { get; set; }
        public virtual Board Board { get; set; }
        public Guid BoardId { get; set; }
        public byte[] Data { get; set; }
    }
}