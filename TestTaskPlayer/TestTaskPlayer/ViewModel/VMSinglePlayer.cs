using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TestTaskPlayer.Data;


namespace TestTaskPlayer.ViewModel
{
    class VMSinglePlayer : INotifyPropertyChanged
    {
        public ICommand PlayCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public Video vid = new Video() { PathVideo = "Y:/Игры/ПППираты.wmv" };
        public BitmapSource frm;
        private bool _activePlayer = false;
        private bool _pathWindow = true;
        private string _title = "";
        private double fps=0;

        public BitmapSource Fram
        {
            get
            {
                return frm;
            }

            set
            {
                frm = value;
                OnPropertyChanged("Fram");
            }
        }
        public Video Vid
        {
            get
            {
                return vid;
            }

            set
            {
                vid = value;
                OnPropertyChanged("Vid");
            }
        }
         public bool ActivePlayer
            {
            get => _activePlayer;
            set
            {
                _activePlayer = value;
                OnPropertyChanged("ActivePlayer");
            }
            }
        public bool PathWindow
        {
            get => _pathWindow;
            set
            {
                _pathWindow = value;
                OnPropertyChanged("PathWindow");
            }
        }
        public  string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public VMSinglePlayer()
        {
            PlayCommand = new DelegateCommand(Play);
            StopCommand = new DelegateCommand(Stop);
            BackCommand = new DelegateCommand(Back);
            OpenCommand = new DelegateCommand(Open);
            fps = vid.Fps;
        }
       


        public void Play(object obj)
        {
            FramesCatch(Vid,1);
            vid.IsPlaying = true;
        }
        public void Stop(object obj)
        {
            vid.IsPlaying = false;
        }
        public void Back(object obj)
        {
            ActivePlayer = false;
            PathWindow = true;
            vid.IsPlaying = false;
            vid = null;
        }
        public void Open(object obj)
        {
            ActivePlayer = true;
            vid = new Video() { PathVideo = Title };
            Play(obj);
            PathWindow = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ReadFrames(Video vid, int k)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate
            {
                Fram = vid.ReadFrames();

                return null;
            }), null);

        }
        private async void FramesCatch(Video vid,  int k)
        {
            
            while (vid.UsedFrames < vid.Frames && vid.IsPlaying)
            {
                ReadFrames(vid, k);
                vid.UsedFrames += 1;
                await Task.Delay(1000 / Convert.ToInt16(fps));
            }

        }

    }
}
