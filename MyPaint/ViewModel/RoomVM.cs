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
    public class RoomVM : NotifyPropertyChanged
    {
        private bool isOpen;
        private Room Room { get; set; }
        public ObservableCollection<ScrimVM> ScrimVMs { get; set; }
        
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }
        public RoomVM(Room room)
        {
            Room = room;
            ScrimVMs = new ObservableCollection<ScrimVM>();
            foreach (Scrim scrim in Room.Scrims)
            {
                ScrimVMs.Add(new ScrimVM(scrim));
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
        public string Icon
        {
            get { return Room.Icon; }
            set
            {
                Room.Icon = value;
                OnPropertyChanged("Icon");
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
