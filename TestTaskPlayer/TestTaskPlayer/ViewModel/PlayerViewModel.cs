using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestTaskPlayer.ViewModel
{
    public class PlayerViewModel 
    {
        public ICommand ExitCommand { get; private set; }
  
        public PlayerViewModel()
        {
            ExitCommand = new DelegateCommand(Exit);                     
        }

        private void Exit(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
    }
}
