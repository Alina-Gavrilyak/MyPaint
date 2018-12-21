using MyPaint.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    public class Room : NotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Scrim> Scrims { get; set; } = new List<Scrim>();
        public List<User> Users { get; set; } = new List<User>();

    }
}
