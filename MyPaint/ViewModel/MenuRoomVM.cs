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
    public class MenuRoomVM : NotifyPropertyChanged
    {
        private bool isOpen;
        public Room Room { get; set; }
        public ObservableCollection<MenuScrimVM> ScrimVMs { get; set; }
        
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }
        public MenuRoomVM(Room room)
        {
            Room = room;
            ScrimVMs = new ObservableCollection<MenuScrimVM>();
            foreach (Scrim scrim in Room.Scrims)
            {
                ScrimVMs.Add(new MenuScrimVM(scrim));
            }
        }
        public string Name
        {
            get { return Room.Name; }
            set
            {
                Room.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public void ChangeName()
        {

        }

        public void AddScrim()
        {

        }
    }
}
