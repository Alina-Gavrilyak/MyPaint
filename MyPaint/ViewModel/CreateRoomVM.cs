using MyPaint.Model;
using MyPaint.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.ViewModel
{
    public class CreateRoomVM : NotifyPropertyChanged
    {
        public ObservableCollection<UserVM> UserVMs { get; set; }
        public List<UserVM> SelectedUserVMs { get; set; }
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
        public CreateRoomVM()
        {
            SelectedUserVMs = new List<UserVM>();
            UserVMs = new ObservableCollection<UserVM>
            {
                new UserVM(new User{Name="Таня" , Icon=@"../Resources/user.png"}),
                new UserVM(new User{Name="Вася", Icon=@"../Resources/user.png" } ),
                new UserVM(new User{Name="Петя", Icon=@"../Resources/user.png" } ),
                new UserVM(new User{Name="Аня", Icon=@"../Resources/user.png" })
            };
        }
    }
}
