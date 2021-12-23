using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TestTaskPlayer.Data;
using TestTaskPlayer.ViewModel;

namespace TestTaskPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
    {
        private double fps;
        private double frames;
        ObservableCollection<NewItem> items = new ObservableCollection<NewItem>();
        Video[] videos = new Video[4];
        Grid[] grids;
        Image[] images;
        public MainWindow()
        {
            InitializeComponent();
            shapka.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
            OpenFirstCommand = new DelegateCommand(OpenFirst);
            OpenSecondCommand = new DelegateCommand(OpenSecond);
            OpenThirdCommand = new DelegateCommand(OpenThird);
            OpenFourthCommand = new DelegateCommand(OpenFourth);
            ExitCommand = new DelegateCommand(Exit);

            //items.Add(new NewItem() { Title = "", Command = OpenFirstCommand  });
            //items.Add(new NewItem() { Title = "", Command = OpenSecondCommand });
            //items.Add(new NewItem() { Title = "", Command = OpenThirdCommand });
            //items.Add(new NewItem() { Title = "", Command = OpenFourthCommand });
            //OpenMenu.ItemsSource = items;
            grids = new Grid[] {/* FirstWindow,*/ /*SecondWindow, ThirdWindow, FourthWindow*/ };
            images = new Image[] { /*FirstImage,*/ /*SecondImage, ThirdImage, FourthImage*/ };

        }

        public ICommand OpenFirstCommand { get; private set; }
        public ICommand OpenSecondCommand { get; private set; }
        public ICommand OpenThirdCommand { get; private set; }
        public ICommand OpenFourthCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        

        public void OpenFirst(object obj)
        {
            Choise(0);
        }
        public void OpenSecond(object obj)
        {
            Choise(1);
        }
        public void OpenThird(object obj)
        {
            Choise(2);
        }
        public void OpenFourth(object obj)
        {
            Choise(3);
        }
        private void Exit(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public void Choise(int k)
        {
            string str = items[k].Title;
            string check = str.Replace(@"\", @"/");
            VideoCapture capture_check = new VideoCapture(items[k].Title);
            if (capture_check.FrameCount == 0)
            {
                MessageBox.Show("Введен неправильный путь к файлу");
                capture_check = null;
            }
            else
            {
                capture_check = null;
                videos[k] = new Video() { PathVideo = check };
                grids[k].Visibility = Visibility.Visible;
                fps = videos[k].Fps;
                frames = videos[k].Frames;
                FramesCatch(videos[k], images, k);
            }
        }

        private void ReadFrames(Video vid, Image[] images, int k)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate
            {
                images[k].Source = vid.ReadFrames();

                return null;
            }), null);
        }



        private async void FramesCatch(Video vid, Image[] images, int k)
        {
            int i = CountOpenVideos();
            while (videos[k].UsedFrames < videos[k].Frames && vid.IsPlaying)
            {
                ReadFrames(vid, images, k);
                videos[k].UsedFrames += 1;
                await Task.Delay(1000 / (Convert.ToInt16(fps) * (i+1)));
            }
           
        }
        
        private int CountOpenVideos()
        {
            int count = 4;
            for(int i = 0; i < 4; i++)
            {
                if (videos[i] == null)
                {
                    count--;
                }
            }
            return count;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            int msg = Convert.ToInt32(((Button)sender).Tag);
            videos[msg].IsPlaying = true;
            FramesCatch(videos[msg], images, msg);
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            int msg = Convert.ToInt32(((Button)sender).Tag);
            videos[msg].IsPlaying = false;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            int msg = Convert.ToInt32(((Button)sender).Tag);
            videos[msg].IsPlaying = false;
            grids[msg].Visibility = Visibility.Hidden;
        }
    }
}
