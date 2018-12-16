using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public ObservableCollection<Image> Image { get; set; }
    }
}
