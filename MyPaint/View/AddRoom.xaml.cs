using MyPaint.Model;
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
using static MyPaint.MenuWindow;

namespace MyPaint.View
{
    /// <summary>
    /// Логика взаимодействия для AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        private CreateRoomVM context;
        CreateRoomDelegate delegat;
        public AddRoom(CreateRoomDelegate delegat,string token)
        {
            InitializeComponent();
            context = new CreateRoomVM(token);
            DataContext = context;
            this.delegat = delegat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = new List<User>();
            foreach (UserVM u in context.SelectedUserVMs)
                users.Add(u.User);

            delegat(users, context.Name );
            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            context.SelectedUserVMs.AddRange(e.AddedItems.Cast<UserVM>().ToList());
            foreach (UserVM u in e.RemovedItems)
                context.SelectedUserVMs.Remove(u);
        }

    }
}
