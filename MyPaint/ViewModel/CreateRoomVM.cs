using MyPaint.Model;
using MyPaint.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyPaint.ViewModel
{
    public class CreateRoomVM : NotifyPropertyChanged
    {
        HttpClient client;
        public ObservableCollection<UserVM> UserVMs { get; set; } = new ObservableCollection<UserVM>();
        public List<UserVM> SelectedUserVMs { get; set; } = new List<UserVM>();
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
        public CreateRoomVM(string token)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51769");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            GetUsers();
        }

        public async Task GetUsers()
        {
            HttpResponseMessage response = await client.GetAsync("/api/Paint/GetUsers");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<User> users =  JsonConvert.DeserializeObject<List<User>>(json);
                foreach(User u in users)
                {
                    UserVMs.Add(new UserVM(u));
                }
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");
        }
    }
}
