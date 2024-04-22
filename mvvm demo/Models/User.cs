using System.ComponentModel;

namespace mvvm_demo.Models
{
    public class User: INotifyPropertyChanged
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged("Username"); }
        }
        private string _Username;

		public string Username
		{
			get { return _Username; }
			set { _Username = value; OnPropertyChanged("Username"); }
		}
		private string _Password;

		public string Password
		{
			get { return _Password; }
			set { _Password = value; OnPropertyChanged("Paasword"); }
		}
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
