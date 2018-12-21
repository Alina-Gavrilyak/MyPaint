using ClientServerClassLibrary;
using Microsoft.AspNet.SignalR.Client;
using MyPaint.Model;
using MyPaint.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Threading;

namespace MyPaint.ViewModel
{
    public class ScrimVM : NotifyPropertyChanged
    {
        private HttpClient client;
        private HubConnection hubConnection;

        public Scrim Scrim { get; set; }
        public string Name
        {
            get { return Scrim.Name; }
            set
            {
                Scrim.Name = value;
                OnPropertyChanged();
            }
        }
        public StrokeCollection Strokes { get; set; } = new StrokeCollection();

        public ScrimVM(Scrim scrim, string token)
        {
            Strokes.StrokesChanged += StrokesChanged;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51769");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            Scrim = scrim;
            SetUpScrimVM(token);
        }

        public async Task SetUpScrimVM(string token)
        {
            InitializeStrokes();
            await SetUpSignalR(token);
        }

        private async Task SetUpSignalR(string token)
        {
            hubConnection = new HubConnection("http://localhost:51769/signalr");
            hubConnection.Headers.Add("Authorization", "Bearer " + token);
            IHubProxy paintHubProxy = hubConnection.CreateHubProxy("PaintHub");
            paintHubProxy.On<StrokeData>("AddStroke", strokeData => OnStrokeAdded(strokeData));
            paintHubProxy.On<Guid>("RemoveStroke", id => OnStrokeRemoved(id));
            await hubConnection.Start();
        }

        private async Task InitializeStrokes()
        {
            Scrim.StrokeIds.AddRange(await GetStrokeIdsFromServer());
            foreach (StrokeId si in Scrim.StrokeIds)
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Strokes.Add(si.Stroke);
                });

        }

        private void OnStrokeAdded(StrokeData strokeData)
        {
            if (strokeData.BoardId != Scrim.Id)
                return;
            StrokeId strokeId = ConvertStrokeDataToStrokeId(strokeData);
            Scrim.StrokeIds.Add(strokeId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Strokes.Add(strokeId.Stroke);
            });
        }
        private void OnStrokeRemoved(Guid id)
        {
            StrokeId strokeId = Scrim.StrokeIds.FirstOrDefault(s => s.Id == id);
            if (strokeId == null)
                return;
            Scrim.StrokeIds.Remove(strokeId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Strokes.Remove(strokeId.Stroke);
            });
        }

        private async Task<List<StrokeId>> GetStrokeIdsFromServer()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/Paint/GetStrokes", Scrim.Id.ToString());
            if (response.IsSuccessStatusCode)
            {
                List<StrokeId> strokeIds = new List<StrokeId>();
                string json = await response.Content.ReadAsStringAsync();
                List<StrokeData> strokeDatas = JsonConvert.DeserializeObject<List<StrokeData>>(json);
                foreach (StrokeData sd in strokeDatas)
                    strokeIds.Add(ConvertStrokeDataToStrokeId(sd));
                return strokeIds;
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");
            return new List<StrokeId>();
        }
        private StrokeId ConvertStrokeDataToStrokeId(StrokeData strokeData)
        {
            StrokeCollection strokes;
            using (MemoryStream ms = new MemoryStream(strokeData.Data))
            {
                strokes = new StrokeCollection(ms);
            }
            return new StrokeId()
            {
                Id = strokeData.Id,
                Stroke = strokes[0]
            };
        }

        private byte[] ConvertStrokeToByteArray(Stroke stroke)
        {
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                new StrokeCollection(new List<Stroke> { stroke }).Save(ms);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            foreach (Stroke s in e.Added)
            {
                s.StylusPointsChanged += StylusPointsChanged;
                AddStroke(s);
            }
            foreach (Stroke s in e.Removed)
            {
                s.StylusPointsChanged -= StylusPointsChanged;
                RemoveStroke(s);
            }
        }

        private async void AddStroke(Stroke stroke)
        {
            if (Scrim.StrokeIds.FirstOrDefault(x => x.Stroke == stroke) != null)
                return;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Paint/AddStroke", new AddStrokeData() { Data = ConvertStrokeToByteArray(stroke), BoardId = Scrim.Id });
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Guid strokeId = JsonConvert.DeserializeObject<Guid>(json);

                StrokeId strokeIdObject = new StrokeId()
                {
                    Id = strokeId,
                    Stroke = stroke
                };

                Scrim.StrokeIds.Add(strokeIdObject);
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");
        }

        private async void RemoveStroke(Stroke stroke)
        {
            if (Scrim.StrokeIds.FirstOrDefault(x => x.Stroke == stroke) == null)
                return;
            StrokeId strokeId = Scrim.StrokeIds.First(x => x.Stroke == stroke);
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Paint/RemoveStroke", strokeId.Id);
            if (response.IsSuccessStatusCode)
            {
                Scrim.StrokeIds.Remove(strokeId);
            }
            else
                MessageBox.Show(response.RequestMessage.ToString(), "RequestError");
        }


        private void StylusPointsChanged(object sender, EventArgs e)
        {
            //ChangeStrokeOnServer
        }
    }
}
