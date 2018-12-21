using ClientServerClassLibrary;
using MyPaint.Model;
using MyPaint.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyPaint.ViewModel
{
    public class MenuVM : NotifyPropertyChanged
    {
        private HttpClient client;
        private MenuRoomVM selectedRoomVM;
        public ObservableCollection<MenuRoomVM> RoomVMs { get; set; } = new ObservableCollection<MenuRoomVM>();

        public ObservableCollection<MenuImageVM> ImageVMs { get; set; } = new ObservableCollection<MenuImageVM>();

        public MenuRoomVM SelectedRoomVM
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

        public MenuVM(string token)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51769");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task SetUpRoomVMs()
        {
            foreach (Room r in await GetRooms())
                RoomVMs.Add(new MenuRoomVM(r));
        }

        private async Task<List<Room>> GetRooms()
        {
            HttpResponseMessage response = await client.GetAsync("/api/Paint/GetRooms");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Room>>(json);
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");
            return new List<Room>();
        }
    }
}
