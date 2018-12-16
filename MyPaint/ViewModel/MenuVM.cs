using MyPaint.Model;
using MyPaint.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint.ViewModel
{
    public class MenuVM : NotifyPropertyChanged
    {
        private RoomVM selectedRoomVM;
        public ObservableCollection<RoomVM> RoomVMs { get; set; }
        
        public ObservableCollection<ImageVM> ImageVMs { get; set; }

        public RoomVM SelectedRoomVM
        {
            get => selectedRoomVM;
            set
            {
                if (selectedRoomVM != null)
                    selectedRoomVM.IsOpen = false;
                selectedRoomVM = value;
                if (selectedRoomVM != null)
                    selectedRoomVM.IsOpen = true;
                OnPropertyChanged();
            }
        }
        public MenuVM()
        {
            RoomVMs = new ObservableCollection<RoomVM>
            {
                new RoomVM(new Room{Name="Друзья" , Icon=@"../Resources/home.png"}),
                new RoomVM(new Room{Name="Семья", Icon=@"../Resources/user.png" } ),
                new RoomVM(new Room{Name="ИА-61", Icon=@"../Resources/home.png" } ),
                new RoomVM(new Room{Name="Работа", Icon=@"../Resources/user.png" })
            };
            ImageVMs = new ObservableCollection<ImageVM>
            {
                new ImageVM(new Image{Name="Ёжик" , URLImage=@"../Resources/image.jpg"}),
                new ImageVM(new Image{Name="Ёжик", URLImage=@"../Resources/image.jpg" } ),
                new ImageVM(new Image{Name="Ёжик", URLImage=@"../Resources/image.jpg" } ),
                new ImageVM(new Image{Name="Ёжик", URLImage=@"../Resources/image.jpg" })
            };
        }
    }
}
