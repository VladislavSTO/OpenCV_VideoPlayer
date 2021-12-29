using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace TestTaskPlayer.ViewModel
{
	class MainWindowVM : BaseViewModel
	{
		private bool _mainWindowState = false;
		private string _userSampleChoise = "Выберите тип сетки";

		public MainWindowVM()
		{
			ExitCommand = new DelegateCommand(Exit);
			ExplandCommand = new DelegateCommand(Expland);
			MinimizeCommand = new DelegateCommand(Minimize);
			SampleChoised = SampleType.Type2X2;
			FullScreenMode = false;
			//

			Players = new ObservableCollection<VMSinglePlayer>();

			for (var i = 0; i < 9; i++)
			{
				var player = new VMSinglePlayer();
				Players.Add(player);
				player.PlayerDoubleClicked += () => PlayerOnPlayerDoubleClicked(player);
			}
		}

		private void PlayerOnPlayerDoubleClicked(VMSinglePlayer player)
		{
			FullScreenDataContext = player;
			FullScreenMode = !FullScreenMode;
		}

		public ICommand ExplandCommand { get; }

		public ICommand ExitCommand { get; private set; }

		public ICommand MinimizeCommand { get; private set; }

		public ObservableCollection<VMSinglePlayer> Players
		{
			get => NotifyPropertyGet(() => Players);
			set => NotifyPropertySet(() => Players, value);
		}

		public bool FullScreenMode
		{
			get => NotifyPropertyGet(() => FullScreenMode);
			set => NotifyPropertySet(() => FullScreenMode, value);
		}

		public VMSinglePlayer FullScreenDataContext
		{
			get => NotifyPropertyGet(() => FullScreenDataContext);
			set => NotifyPropertySet(() => FullScreenDataContext, value);
		}

		public SampleType SampleChoised
		{
			get => NotifyPropertyGet(() => SampleChoised);
			set => NotifyPropertySet(() => SampleChoised, value);
		}

		public string UserSampleChoise
		{
			get => _userSampleChoise;
			set
			{
				_userSampleChoise = value;
				ChangeSampleChoised();
			}
		}

		private void Exit(object obj)
		{
			Environment.Exit(1);
		}

		private void Expland(object obj)
		{
			if (!_mainWindowState)
			{
				Application.Current.MainWindow.WindowState = WindowState.Maximized;
				_mainWindowState = true;
			}
			else
			{
				Application.Current.MainWindow.WindowState = WindowState.Normal;
				_mainWindowState = false;
			}

		}

		private void ChangeSampleChoised()
		{
			switch (_userSampleChoise)
			{
				case "Сетка 2х2":
					SampleChoised = SampleType.Type2X2;
					break;
				case "Сетка 3х3":
					SampleChoised = SampleType.Type3X3;
					break;
				case "Сетка 1:5":
					SampleChoised = SampleType.TypeOneBigFiveSmall;
					break;
				case "Сетка 2:6":
					SampleChoised = SampleType.TypeTwoBigSixSmall;
					break;
				default:
					SampleChoised = SampleType.Type2X2;
					break;
			}
		}
		private void Minimize(object obj)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}


	}
}
