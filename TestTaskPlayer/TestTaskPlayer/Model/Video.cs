using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace TestTaskPlayer.Model
{
	public class Video 
    {
	    private Mat _oneMatFrame;
        private VideoCapture _capture;
        private string _pathVideo = " ";

        public double Fps => _capture.Fps;

        public double Frames => _capture.FrameCount;

        public bool IsPlaying { get; set; } = true;

        public string PathVideo
        {
            get => _pathVideo;
            set
            {
                _pathVideo = value;
                _capture = new VideoCapture(_pathVideo);
            }
        }
        
        public int UsedFrames { get; set; } = 1;

        public BitmapSource ReadFrames()
        {
            if (IsPlaying)
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
