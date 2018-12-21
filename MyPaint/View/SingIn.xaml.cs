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

namespace MyPaint.View
{
    /// <summary>
    /// Логика взаимодействия для SingIn.xaml
    /// </summary>
    public partial class SingIn : Window
    {
        public SingIn()
        {
            InitializeComponent();
        }
        
        private async void btn_SingIn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51769");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/Token");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
            keyValues.Add(new KeyValuePair<string, string>("username", tb_UserName.Text));
            keyValues.Add(new KeyValuePair<string, string>("password", pb_Password.Password));

            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage signInResponse = await client.SendAsync(request);
            if (signInResponse.IsSuccessStatusCode)
            {
                string responseJson = await signInResponse.Content.ReadAsStringAsync();
                dynamic responseData = JsonConvert.DeserializeObject(responseJson);
                string token = responseData.access_token;

                MenuWindow menuWindow = new MenuWindow(token);
                menuWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show(signInResponse.StatusCode.ToString() + signInResponse.RequestMessage.ToString(), "Ошибка запроса");
        }

        private void btn_SingUp_Click(object sender, RoutedEventArgs e)
        {
            SingUp s = new SingUp();
            s.Show();
            this.Close();
        }
    }
}
