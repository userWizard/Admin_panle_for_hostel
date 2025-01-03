using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=Nikita; Initial Catalog=RegHostel; Integrated Security=True");
        int counter = 0;


        public MainWindow()
        {

            InitializeComponent();
            conn.Open();

            Console.WriteLine(conn.State);
        }

        private void OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private SqlConnection getConnection()
        {
            return conn;
        }

        private void LogIN_Click(object sender, RoutedEventArgs e)
        {
            string login = Login1.Text;
            string password = Password.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Поля Логин и Пароль являются обязательными.");
                return;
            }
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Login = @Login AND Password = @Password", conn);
            cmd.Parameters.Add("@login", login);
            cmd.Parameters.Add("@password", password);
            int result = ((int)cmd.ExecuteScalar());

            if (counter < 3)
            {
                if (result == 0)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                    counter++;
                }
                else
                {
                    MessageBox.Show("Вы успешно авторизироавались!");
                    SqlCommand cmd2 = new SqlCommand("SELECT RoleID FROM Users WHERE Login = @login1 AND Password = @password1", conn);
                    cmd2.Parameters.Add("@login1", login);
                    cmd2.Parameters.Add("@password1", password);
                    int result2 = ((int)cmd2.ExecuteScalar());

                    if (result2 == 1) 
                    {
                        Admin admin_panel = new Admin();
                        admin_panel.Show();
                        Login1.Clear();
                        Password.Clear();
                    }

                    else if (result2 == 2)
                    {
                        User user_panel = new User();
                        user_panel.Show();
                        Login1.Clear();
                        Password.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы исчерпали лимит авторизаций!");
            }
        }
    }
}
