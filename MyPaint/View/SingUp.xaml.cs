using ClientServerClassLibrary;
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
    /// Логика взаимодействия для SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        public SingUp()
        {
            InitializeComponent();
           
        }
        
        private async void btn_SingIn_Click(object sender, RoutedEventArgs e)
        {
            string password1 = pb_Password1.Password;
            string password2 = pb_Password2.Password;

            if (password1.Equals(password2))
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:51769");
                HttpResponseMessage signUpResponse = await client.PostAsJsonAsync("/api/paint/SignUp", new RegistrationData { UserName = tb_UserName.Text, Password = password1 });
                if (signUpResponse.IsSuccessStatusCode)
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/Token");

                    var keyValues = new List<KeyValuePair<string, string>>();
                    keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
                    keyValues.Add(new KeyValuePair<string, string>("username", tb_UserName.Text));
                    keyValues.Add(new KeyValuePair<string, string>("password", password1));

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
                else
                    MessageBox.Show(signUpResponse.StatusCode.ToString() + signUpResponse.RequestMessage.ToString(), "Ошибка запроса");
            }
            else
                MessageBox.Show("Не совпадают пароли", "Ошибка");
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            SingIn s = new SingIn();
            s.Show();
            this.Close();
        }
    }
}
