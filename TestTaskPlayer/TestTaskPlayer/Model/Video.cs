using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestTaskPlayer.Data
{
 
    public class Video 
    {
        bool isPlaying = true;
        Mat m;
        VideoCapture capture;
        int usedframes = 1;
        public string pathvideo = " ";

        public double Fps 
        {
            get
            {
                return capture.Fps;
            }
        }
 
        public double Frames
        {
            get
            {
                return capture.FrameCount;
            }

        }

        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }
            set
            {
                isPlaying = value;
            }
        }

        public string PathVideo
        {
            get
            {
                return pathvideo;
            }
            set
            {
                pathvideo = value;
                capture = new VideoCapture(pathvideo);
            }
        }

        public int UsedFrames
        {
            get
            {
                return usedframes;
            }
            set
            {
                usedframes = value;
            }
        }

        public BitmapSource ReadFrames()
        {
            if (isPlaying)
            {
                m = new Mat();
                capture.Read(m);

                if (capture.Read(m) != false)
                {
                    return BitmapSourceConverter.ToBitmapSource(m);
                }
                else
                    return null;
            }
            return null;
        }
    }
}
