using Microsoft.Xaml.Behaviors.Core;
using mvvm_demo.Views;
using System.Windows;
using System.Windows.Input;


namespace mvvm_demo.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        //tao command thay cho event handler
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand Feature1Command { get; set; }
        public ICommand Feature2Command { get; set; }
        public ICommand Feature3Command { get; set; }
        public ICommand Feature4Command { get; set; }
        public ICommand Feature5Command { get; set; }
        //dinh nghia ham xu ly
        public MainViewModel() 
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM != null)
                {
                    if (loginVM.isLogin)
                    {
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }
                }
            }
              );
            Feature1Command = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            Feature2Command = new RelayCommand<object>((p) => { return true; }, (p) => { Window2 wd = new Window2(); wd.ShowDialog(); });
            Feature3Command = new RelayCommand<object>((p) => { return true; }, (p) => { Window3 wd = new Window3(); wd.ShowDialog(); });
            Feature4Command = new RelayCommand<object>((p) => { return true; }, (p) => { Window4 wd = new Window4(); wd.ShowDialog(); });
            Feature5Command = new RelayCommand<object>((p) => { return true; }, (p) => { Window5 wd = new Window5(); wd.ShowDialog(); });
        }
    }
}
