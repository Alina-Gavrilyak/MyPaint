using ClientServerClassLibrary;
using MyPaint.Model;
using MyPaint.View;
using MyPaint.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        HttpClient client;
        string token;

        public MenuWindow(string token)
        {
            InitializeComponent();

            this.token = token;

            context = new MenuVM(token);
            DataContext = context;
            context.SetUpRoomVMs();

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51769");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            RoomListBox.UnselectAll();
        }

        private void CreateRoomButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRoomDelegate delegat = CreateRoom;
            AddRoom ar = new AddRoom(delegat,token);
            ar.Show();

        }

        public delegate void CreateRoomDelegate(List<User> users, string name);
        public async void CreateRoom(List<User> users, string name)
        {
            List<Guid> ids = new List<Guid>();
            foreach (User u in users)
                ids.Add(u.Id);
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Paint/AddRoom", new AddRoomData() { Name = name, UsersId = ids });
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Guid roomId = JsonConvert.DeserializeObject<Guid>(json);

                MenuRoomVM roomVM = new MenuRoomVM(new Room() { Name = name, Users = users, Id= roomId });
                context.RoomVMs.Add(roomVM);
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");


        }

        public delegate void CreateScrimDelegate(string name, MenuRoomVM roomVM);
        public async void CreateScrim(string name, MenuRoomVM roomVM)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Paint/AddBoard", new AddBoardData() { BoardName = name, RoomId = roomVM.Room.Id });
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Guid scrimId = JsonConvert.DeserializeObject<Guid>(json);

                Scrim s = new Scrim() { Name = name, Id = scrimId };

                MenuScrimVM scrimVM = new MenuScrimVM(s);
                roomVM.ScrimVMs.Add(scrimVM);

                MainWindow mw = new MainWindow(new ScrimVM(s,token));
                mw.Show();
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");
        }
        private void btnAddScrim_Click(object sender, RoutedEventArgs e)
        {
            CreateScrimDelegate delegat = CreateScrim;
            CreateScrimWindow cs = new CreateScrimWindow(delegat, context.SelectedRoomVM);
            context.SelectedRoomVM = null;
            cs.Show();
        }

        private void OpenScrimeButton_Click(object sender, RoutedEventArgs e)
        {
            Scrim s =((MenuScrimVM)((Button)sender).DataContext).Scrim;
            MainWindow mw = new MainWindow(new ScrimVM(s,token));
            mw.Show();
        }
    }
}
