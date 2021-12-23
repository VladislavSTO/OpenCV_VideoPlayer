using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestTaskPlayer.ViewModel
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private VMSinglePlayer _player1;
        private VMSinglePlayer _player2;
        private VMSinglePlayer _player3;
        private VMSinglePlayer _player4;

        public MainWindowVM()
        {
            Player1 = new VMSinglePlayer();
            Player2 = new VMSinglePlayer();
            Player3 = new VMSinglePlayer();
            Player4 = new VMSinglePlayer();
            ExitCommand = new DelegateCommand(Exit);
        }
        public ICommand ExitCommand { get; private set; }
        private void Exit(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public VMSinglePlayer Player1
        {
            get => _player1;
            set
            {
                _player1 = value;
                OnPropertyChanged("Player1");
            }
        }
        public VMSinglePlayer Player2
        {
            get => _player2;
            set
            {
                _player2 = value;
                OnPropertyChanged("Player2");
            }
        }
        public VMSinglePlayer Player3
        {
            get => _player3;
            set
            {
                _player3 = value;
                OnPropertyChanged("Player3");
            }
        }
        public VMSinglePlayer Player4
        {
            get => _player4;
            set
            {
                _player4 = value;
                OnPropertyChanged("Player4");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
