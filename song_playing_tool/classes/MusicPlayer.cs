using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
// using Microsoft.Win32;
using System.Windows.Forms;
using System.Windows.Media;

namespace song_playing_tool.classes
{
    public class MusicPlayer
    {
        public MediaPlayer soundPlayer;
        private string filePath = string.Empty;
        private Timer tmrWmpPlayerPosition;
        private bool isPlaying = false;
        public TimeSpan StartTime { get; set; }
        public TimeSpan StopTime { get; set; }
        public string SongName { get; set; }
        public bool HasStarted { get; set; } = false;

        public MusicPlayer()
        {
            soundPlayer = new MediaPlayer();
        }


        public bool SearchSong()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\bartv\\Music";
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                // filePath = "C:\\Users\\bartv\\Music\\wywh.mp3";
                filePath = openFileDialog.FileName;
                SongName = openFileDialog.SafeFileName;
                try
                {
                    soundPlayer.Stop();
                    soundPlayer.Close();
                    // Load the .mp3 file.
                    soundPlayer.Open(new Uri(filePath));
                    if (!HasStarted)
                    {
                        StopWmpPlayerTimer();
                        StartWmpPlayerTimer();
                    }
                    else
                    {
                        HasStarted = false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("wubba lubba something something load song: " + ex.Message);
                }
            }

            return false;
        }

        public void PlaySong()
        {
            soundPlayer.Play();
            this.isPlaying = true;
        }

        public void PauseSong()
        {
            soundPlayer.Pause();
            this.isPlaying = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime">time in seconds</param>
        /// <param name="stopTime">time in seconds</param>
        public void PlayPause(TimeSpan startTime, TimeSpan stopTime)
        {
            if (!HasStarted)
            {
                StartTime = startTime;
                soundPlayer.Position = StartTime;
                StopTime = stopTime;
                HasStarted = true;
            }

            if (!isPlaying)
            {
                PlaySong();
            }
            else
            {
                PauseSong();
            }
        }

        private void tmrWmpPlayerPosition_Tick(object sender, EventArgs e)
        {
            if (StopTime > soundPlayer.Position) return;
            soundPlayer.Position = StartTime;
            // if (notloop....)
            // {
                // StopWmpPlayerTimer();
            // }
        }

        private void StartWmpPlayerTimer()
        {
            tmrWmpPlayerPosition = new Timer();
            tmrWmpPlayerPosition.Tick += new EventHandler(tmrWmpPlayerPosition_Tick);
            tmrWmpPlayerPosition.Enabled = true;
            tmrWmpPlayerPosition.Interval = 500;
            tmrWmpPlayerPosition.Start();
        }

        private void StopWmpPlayerTimer()
        {
            if (tmrWmpPlayerPosition != null)
                tmrWmpPlayerPosition.Dispose();
            tmrWmpPlayerPosition = null;
        }
    }
}