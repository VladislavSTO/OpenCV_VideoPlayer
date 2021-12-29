using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.Windows.Media.Imaging;

namespace TestTaskPlayer.Data
{
	public class Video 
    {
        private bool _isPlaying = true;
        private Mat _oneMatFrame;
        private VideoCapture _capture;
        private int _usedFrames = 1;
        private string _pathVideo = " ";

        public double Fps => _capture.Fps;

        public double Frames => _capture.FrameCount;

        public bool IsPlaying
        {
            get => _isPlaying;
            set => _isPlaying = value;
        }

        public string PathVideo
        {
            get => _pathVideo;
            set
            {
                _pathVideo = value;
                _capture = new VideoCapture(_pathVideo);
            }
        }
        
        public int UsedFrames
        {
            get => _usedFrames;
            set => _usedFrames = value;
        }

        public BitmapSource ReadFrames()
        {
            if (_isPlaying)
            {
                _oneMatFrame = new Mat();
                var success =  _capture.Read(_oneMatFrame);
                if (success)
                {
                    return _oneMatFrame.ToBitmapSource();
                }
            }
            return null;
        }
    }
}
