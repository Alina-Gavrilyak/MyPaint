using MyPaint.Model;
using MyPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.ViewModel
{
    public class UserVM : NotifyPropertyChanged
    {
        public User User { get; set; }

        public UserVM(User user)
        {
            User = user;
        }

        public string Name
        {
            get { return User.Name; }
            set
            {
                User.Name = value;
                OnPropertyChanged("Name");
            }
        }
        //public string Icon
        //{
        //    get { return User.Icon; }
        //    set
        //    {
        //        User.Icon = value;
        //        OnPropertyChanged("Icon");
        //    }
        //}
    }
}
