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
using System.Windows.Shapes;

namespace WpfApp1
{

    public partial class Admin : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=Nikita; Initial Catalog=RegHostel; Integrated Security=True");
        public Admin()
        {
            InitializeComponent();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users", conn);
            int result = ((int)cmd.ExecuteScalar());
            for (int count = 1; count <= result; count++)
            {
                SqlCommand cm = new SqlCommand("SELECT Login FROM Users WHERE ID=@count", conn);
                cm.Parameters.Add("@count", count);
                string User = ((string)cm.ExecuteScalar());
                UsersChange.Items.Add(User);
                UsersBlockUnLock.Items.Add(User);
            }


        }

        private void Role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Insert_Btc_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;
            string firstName = FirstName.Text;
            string secondName = SecondName.Text;
            string lastName = LastName.Text;
            string phoneNumber = PhoneNumber.Text;
            int role = Role.SelectedIndex + 1;

            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Login = @login AND Password = @password", conn);
            checkCmd.Parameters.Add("@login", login);
            checkCmd.Parameters.Add("@password", password);
            int userCount = (int)checkCmd.ExecuteScalar();
            if (userCount > 0)
            {
                MessageBox.Show("Пользователь с такими данными уже существует!");
                return;
            }

            
            SqlCommand cmd = new SqlCommand("INSERT INTO Users (Login, Password, RoleID, FirstName, SecondName, LastName, PhoneNumber) VALUES(@login, @password, @RoleID, @firstName, @secondName, @lastName, @phoneNumber)", conn);
            cmd.Parameters.Add("@login", login);
            cmd.Parameters.Add("@password", password);
            cmd.Parameters.Add("@RoleID", role);
            cmd.Parameters.Add("@firstName", firstName);
            cmd.Parameters.Add("@secondName", secondName);
            cmd.Parameters.Add("@lastName", lastName);
            cmd.Parameters.Add("@phoneNumber", phoneNumber);
            cmd.ExecuteScalar();

            MessageBox.Show("Пользователь успешно добавлен!");
        }

        private void UsersChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChangData_Btc_Click(object sender, RoutedEventArgs e)
        {
            int selectedID = UsersChange.SelectedIndex + 1;
            SqlCommand update = new SqlCommand("UPDATE Users SET Login = @selectedLogin, Password = @password WHERE ID = @selectedID", conn);
            update.Parameters.Add("@selectedLogin", Second_Login.Text);
            update.Parameters.Add("@password", Second_Password.Text);
            update.Parameters.Add("@selectedID", selectedID);
            update.ExecuteScalar();
            MessageBox.Show("Данные изменены!");

        }

        private void UsersBlockUnLock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Razban_BTC_Click(object sender, RoutedEventArgs e)
        {
            int selectedID = UsersBlockUnLock.SelectedIndex + 1;
            SqlCommand cmd = new SqlCommand("UPDATE Users SET IsBlocked = 0 WHERE ID = @selectedID", conn);
            cmd.Parameters.AddWithValue("@selectedID", selectedID);
            cmd.ExecuteScalar();
            MessageBox.Show("Пользователь разблокирован!");

        }

        private void Ban_BTC_Click(object sender, RoutedEventArgs e)
        {
            int selectedID = UsersBlockUnLock.SelectedIndex + 1;
            SqlCommand cmd2 = new SqlCommand("UPDATE Users SET IsBlocked = 1 WHERE ID = @selectedID", conn);
            cmd2.Parameters.AddWithValue("@selectedID", selectedID);
            cmd2.ExecuteScalar();
            MessageBox.Show("Пользователь заблокирован!");

        }

    }
}
