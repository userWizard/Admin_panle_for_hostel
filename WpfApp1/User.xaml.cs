using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=Nikita; Initial Catalog=RegHostel; Integrated Security=True");
        public User()
        {
            InitializeComponent();
            conn.Open();
        }


        private void Leav_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        // Кнопака изменения данных в Пользовательской панели
        private void ChangeData_Click(object sender, RoutedEventArgs e)
        {
            string newLogin = NewLogin.Text;
            string newPassword = NewPassword.Text;
            string repPassword = RepPassword.Text;

            // Условия проверки на заполнения Лоигна и Пароля
            if (string.IsNullOrEmpty(newLogin) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(repPassword))
            {
                MessageBox.Show("Поля Логин и Пароль являются обязательными.");
                return;
            }
            else
            {
                // Условия проверки на совпадения паролей
                if (newPassword == repPassword)
                {
                    SqlCommand update = new SqlCommand("UPDATE Users SET Login = @selectedLogin, Password = @password WHERE Login = @selectedID", conn);
                    update.Parameters.Add("@selectedLogin", newLogin);
                    update.Parameters.Add("@password", newPassword);
                    update.Parameters.Add("@selectedID", OldLogin1.Text);
                    update.ExecuteScalar();
                    MessageBox.Show("Данные изменены!");
                }
                // Ошибка "Пароли не совпадают!"
                else
                {
                    MessageBox.Show("Пароли не совпадают!");
                }
            }


        }


        }
    }
