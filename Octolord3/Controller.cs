using EyeOfTheMedusa3;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace App1
{



    class Controller
    {

        bool isDownT;
        bool isUpT;

        Queue<Byte> q;

        private WaveIn waveIn = null;

        
        public Controller() {
            isDownT = false;
            isUpT = false;

            q = new Queue<byte>();

            if (waveIn != null)
                return;

            // create wave input from mic
            waveIn = new WaveIn();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.BufferMilliseconds = 25;
            waveIn.RecordingStopped += waveIn_RecordingStopped;
            waveIn.DataAvailable += waveIn_DataAvailable;

            // start recording and playback
            waveIn.StartRecording();
   
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {

              
            double maxi = 0.05;
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All))
            {

                if (device.State == DeviceState.Active) {
                    maxi = device.AudioMeterInformation.MasterPeakValue;
                }
            }

     
            if (maxi > 0.1)
            {
                isUpT = true;
            }
            else isUpT = false;

            if (maxi < 0.03)
            {

                isDownT = true;
            }
            else isDownT = false;     
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (waveIn != null)
                waveIn.StopRecording();
        }

        void waveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            // dispose of wave input
            if (waveIn != null)
            {
                waveIn.Dispose();
                waveIn = null;
            }

           
        }
        
        public void update()
        {
            
        }

        public bool isDown()
        {
            return isDownT;
        }

        internal bool isUp()
        {
            return isUpT;
        }
    }
}
