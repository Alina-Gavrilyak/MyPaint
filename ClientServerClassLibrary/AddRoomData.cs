using System;
using System.Collections.Generic;
using System.Text;

namespace ClientServerClassLibrary
{
    public class AddRoomData
    {
        public List<Guid> UsersId { get; set; }
        public string Name { get; set; }
    }
}
