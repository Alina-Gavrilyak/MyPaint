using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPaint.Model;
using MyPaint.Models;

namespace MyPaint.ViewModel
{
    public class MenuImageVM : NotifyPropertyChanged
    {
        public Image Image { get; set; }

        public MenuImageVM(Image image)
        {
            Image = image;
        }
        public string Name
        {
            get { return Image.Name; }
            set
            {
                Image.Name = value;
                OnPropertyChanged("Image");
            }
        }
        public string URLImage
        {
            get { return Image.URLImage; }
            set
            {
                Image.URLImage = value;
                OnPropertyChanged("URLImage");
            }
        }
    }
}
