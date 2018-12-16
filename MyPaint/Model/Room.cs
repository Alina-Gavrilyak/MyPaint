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
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Scrim> Scrims { get; set; }
        public string Icon { get; set; }
        public List<User> Users { get; set; }
        public Room()
        {
            Scrims = new List<Scrim>
                {
                new Scrim {Name="Первый рисунок"},
                new Scrim {Name="Второй рисунок" },
                new Scrim {Name="Третий рисунок" },
                new Scrim {Name="Первый рисунок" }
            };
        }
    }
}
