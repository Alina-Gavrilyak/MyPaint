using MyPaint.Model;
using MyPaint.View;
using MyPaint.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyPaint
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private MenuVM context;
        public MenuWindow()
        {
            InitializeComponent();

            context = new MenuVM();
            DataContext = context;
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            RoomListBox.UnselectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateRoomDelegate delegat = CreateRoom;
            

            AddRoom ar = new AddRoom(delegat);
            ar.Show();

        }

        public delegate void CreateRoomDelegate(List<User> users, string name);
        public void CreateRoom(List<User> users, string name)
        {
            RoomVM roomVM = new RoomVM(new Room() {Name=name, Users= users});
            context.RoomVMs.Add(roomVM);
        }

        public delegate void CreateScrimDelegate(string name);
        public void CreateScrim(string name)
        {
            ScrimVM scrimVM = new ScrimVM(new Scrim() {Name = name});
            context.SelectedRoomVM.ScrimVMs.Add(scrimVM);

            context.SelectedRoomVM = null;
        }
        private void btnAddScrim_Click(object sender, RoutedEventArgs e)
        {
            CreateScrimDelegate delegat = CreateScrim;

            CreateScrim cs = new CreateScrim(delegat);
            cs.Show();
            context.SelectedRoomVM.IsOpen = false;
        }
    }
}
