using System.Windows;
using System.Windows.Input;
using TestTaskPlayer.ViewModel;

namespace TestTaskPlayer.View
{
	public partial class Player
    {
	    private VMSinglePlayer _vm;

        public Player()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
	        _vm = e.NewValue as VMSinglePlayer;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
	        if (e.ClickCount == 2)
	        {
                _vm.OnPlayerDoubleClicked();
	        }
        }
    }
}
