using MyPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.ViewModel
{
    public class CreateScrimVM : NotifyPropertyChanged
    {
        private string name { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
    }
}
