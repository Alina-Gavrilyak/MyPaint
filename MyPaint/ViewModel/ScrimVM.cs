using MyPaint.Model;
using MyPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.ViewModel
{
    public class ScrimVM : NotifyPropertyChanged
    {
        public Scrim Scrim { get; set; }

        public ScrimVM(Scrim scrim)
        {
            Scrim = scrim;
        }
        public string Name
        {
            get { return Scrim.Name; }
            set
            {
                Scrim.Name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
