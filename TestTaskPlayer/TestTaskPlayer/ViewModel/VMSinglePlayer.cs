using OpenCvSharp;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TestTaskPlayer.Data;


namespace TestTaskPlayer.ViewModel
{
    class VMSinglePlayer : BaseViewModel /*, INotifyPropertyChanged*/
    {
        private Video _vid = new Video() { PathVideo = "Y:/Игры/ПППираты.wmv" };
        private double _fps=0;

        public VMSinglePlayer()
        {
            PlayCommand = new DelegateCommand(Play);
            StopCommand = new DelegateCommand(Stop);
            BackCommand = new DelegateCommand(Back);
            OpenCommand = new DelegateCommand(Open);
            ActivePlayer = false;
            PathWindow = true;
            Title = "";
            _fps = _vid.Fps;
        }

        public BitmapSource Fram
        {
            get => NotifyPropertyGet(() => Fram);
            set => NotifyPropertySet(() => Fram, value);
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

        public ICommand PlayCommand { get; private set; }

        public ICommand StopCommand { get; private set; }

        public ICommand BackCommand { get; private set; }

        public ICommand OpenCommand { get; private set; }

        public void Play(object obj)
        {
            FramesCatch(_vid);
            _vid.IsPlaying = true;
        }

        public void Stop(object obj)
        {
            _vid.IsPlaying = false;
        }

        public void Back(object obj)
        {
            ActivePlayer = false;
            PathWindow = true;
            _vid.IsPlaying = false;
            _vid = null;
        }

        public void Open(object obj)
        {            
            if (Title != "")
            {                
                string check = Title.Replace(@"\", @"/");
                VideoCapture capture_check = new VideoCapture(check);
                if (capture_check.FrameCount == 0)
                    {
                        MessageBox.Show("Введен неправильный путь к файлу");
                        capture_check = null;
                    }
                else
                {
                    ActivePlayer = true;
                    capture_check = null;
                    _vid = new Video() { PathVideo = check };
                    Play(obj);
                    PathWindow = false;
                }
            }
            else
            {
                MessageBox.Show("Введите путь к файлу в строку");
            }           
        }

        private void ReadFrames(Video _vid)
        {
            Application.Current.Dispatcher.Invoke(() => Fram = _vid.ReadFrames());                  

            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate
            //{
            //    Fram = _vid.ReadFrames();
            //    return null;
            //}), null);
        }

        private async void FramesCatch(Video _vid)
        {            
            while (_vid.UsedFrames < _vid.Frames && _vid.IsPlaying)
            {
                ReadFrames(_vid);
                _vid.UsedFrames += 1;
                await Task.Delay(1000 / Convert.ToInt16(_fps));
            }
        }
    }
}
