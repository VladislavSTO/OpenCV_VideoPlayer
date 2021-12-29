using OpenCvSharp;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TestTaskPlayer.Model;


namespace TestTaskPlayer.ViewModel
{
	public class VMSinglePlayer : BaseViewModel
	{
		private Video _vid = new Video();

		public VMSinglePlayer()
		{
			PlayCommand = new DelegateCommand(Play);
			StopCommand = new DelegateCommand(Stop);
			BackCommand = new DelegateCommand(Back);
			OpenCommand = new DelegateCommand(Open);
			ActivePlayer = false;
			PathWindow = true;
			LoadingImage = false;
			Title = "";
		}

		public event Action PlayerDoubleClicked;

		public bool LoadingImage
		{
			get => NotifyPropertyGet(() => LoadingImage);
			set => NotifyPropertySet(() => LoadingImage, value);
		}

		public BitmapSource Frame
		{
			get => NotifyPropertyGet(() => Frame);
			set => NotifyPropertySet(() => Frame, value);
		}

		public bool ActivePlayer
		{
			get => NotifyPropertyGet(() => ActivePlayer);
			set => NotifyPropertySet(() => ActivePlayer, value);
		}

		public bool PathWindow
		{
			get => NotifyPropertyGet(() => PathWindow);
			set => NotifyPropertySet(() => PathWindow, value);
		}

		public string Title
		{
			get => NotifyPropertyGet(() => Title);
			set => NotifyPropertySet(() => Title, value);
		}

		public void OnPlayerDoubleClicked()
		{
			PlayerDoubleClicked?.Invoke();
		}

		public ICommand PlayCommand { get; private set; }

		public ICommand StopCommand { get; private set; }

		public ICommand BackCommand { get; private set; }

		public ICommand OpenCommand { get; private set; }

		public void Play(object obj)
		{
			Play();
		}

		public void Stop(object obj)
		{
			_vid.IsPlaying = false;
		}

		public void Back(object obj)
		{
			Back();
		}

		private void Back()
		{
			ActivePlayer = false;
			PathWindow = true;
			LoadingImage = false;
			_vid.IsPlaying = false;
			_vid = null;
		}

		private void Play()
		{
			LoadingImage = false;
			FramesCatch();
			_vid.IsPlaying = true;
		}

		public async void Open(object obj)
		{
			if (Title != "")
			{
				LoadingImage = true;
				PathWindow = false;
				await Task.Run(OpenFile);
			}
			else
			{
				MessageBox.Show("Введите путь к файлу в строку");
			}
		}

		public void OpenFile()
		{
			string check = Title.Replace(@"\", @"/");
			VideoCapture captureCheck = new VideoCapture(check);
			if (captureCheck.FrameCount == 0)
			{
				MessageBox.Show("Введен неправильный путь к файлу");
				Back();
			}
			else
			{

				_vid = new Video() { PathVideo = check };
				Play();
				ActivePlayer = true;
			}
		}

		private void ReadFrames()
		{
			Application.Current.Dispatcher.Invoke(() => Frame = _vid.ReadFrames());
		}

		private async void FramesCatch()
		{
			if (_vid.Frames > 0)
			{
				while (_vid.UsedFrames < _vid.Frames && _vid.IsPlaying)
				{
					ReadFrames();
					_vid.UsedFrames += 1;
					await Task.Delay(1000 / Convert.ToInt16(_vid.Fps));
				}
			}
			else
			{
				while (_vid.IsPlaying)
				{
					ReadFrames();
					_vid.UsedFrames += 1;
					await Task.Delay(1000 / Convert.ToInt16(_vid.Fps));
				}
			}
		}
	}
}
