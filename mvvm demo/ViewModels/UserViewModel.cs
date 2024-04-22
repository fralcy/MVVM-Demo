using Microsoft.Data.SqlClient;
using mvvm_demo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using mvvm_demo.Resources;
using System.Windows;

namespace mvvm_demo.ViewModels
{
    public class UserViewModel: ViewModelBase
    {
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private User _SelectedItem;
        public User SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ID = SelectedItem.ID;
                    Username = SelectedItem.Username;
                    Password = SelectedItem.Password;
                }
            }
        }
        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UserViewModel()
        {
            List = LoadData();

            AddCommand = new RelayCommand<object>((p) =>
            {
                foreach (var item in List)
                {
                    if (ID == item.ID)
                        return false;
                }
                return true;
            }, (p) =>
            {
                AddData();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                foreach (var item in List)
                {
                    if (item.ID == SelectedItem.ID) return true;
                }
                return false;

            }, (p) =>
            {
                EditData();
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                foreach (var item in List)
                {
                    if (item.ID == SelectedItem.ID) return true;
                }
                return false;

            }, (p) =>
            {
                DeleteData();
            });
        }

        private ObservableCollection<User> LoadData()
        {
            var data = new ObservableCollection<User>();
            using (var connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM TB", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(new User
                        {
                            ID = reader.GetInt32(0),
                            Username = reader.GetString(1), // Assuming Id is the first column (index 0)
                            Password = reader.GetString(2), // Assuming Name is the second column (index 1)
                                                            // Map other properties from reader based on column indexes
                        }) ;
                    }
                }
            }
            return data;
        }

        private void AddData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Example: Execute a query
                    string query = "INSERT INTO TB (ID, USERNAME, PASS) VALUES (@ID, @Username, @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Data inserted successfully
                            MessageBox.Show("Data inserted successfully!"); // Or show a success message in your UI
                        }
                        else
                        {
                            // No data inserted (handle error)
                            MessageBox.Show("Error inserting data!"); // Or display an error message
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            List.Add(new User
            {
                ID = _ID,
                Username = _Username, // Assuming Id is the first column (index 0)
                Password = _Password, // Assuming Name is the second column (index 1)
                                                // Map other properties from reader based on column indexes
            });
        }

        private void EditData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Example: Execute a query
                    string query = "UPDATE TB SET Username = @Username, Pass = @Password WHERE ID = @ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@ID", SelectedItem.ID);
                        command.Parameters.AddWithValue("@Username", SelectedItem.Username);
                        command.Parameters.AddWithValue("@Password", SelectedItem.Password);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Data inserted successfully
                            MessageBox.Show("Data edited successfully!"); // Or show a success message in your UI
                        }
                        else
                        {
                            // No data inserted (handle error)
                            MessageBox.Show("Error editing data!"); // Or display an error message
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            foreach (var item in List)
            {
                if (item.ID == SelectedItem.ID)
                {
                    item.Username = SelectedItem.Username;
                    item.Password = SelectedItem.Password;
                    return;
                }
            }
        }
        private void DeleteData()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Example: Execute a query
                    string query = "DELETE FROM TB WHERE ID = @ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@ID", SelectedItem.ID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Data inserted successfully
                            MessageBox.Show("Data deleted successfully!"); // Or show a success message in your UI
                        }
                        else
                        {
                            // No data inserted (handle error)
                            MessageBox.Show("Error deleting data!"); // Or display an error message
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            foreach (var item in List)
            {
                if (item.ID == SelectedItem.ID)
                {
                    List.Remove(item);
                    return;
                }
            }
        }
    }
}
