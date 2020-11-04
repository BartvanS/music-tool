using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using Microsoft.Win32;
using song_playing_tool.classes;

namespace song_playing_tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MusicPlayer player;
        public MainWindow()
        {
            player = new MusicPlayer();
        }

       

        private void SelectSongBtn_Click(object sender, RoutedEventArgs e)
        {
            if (player.SearchSong())
            {
            }
        }

        private void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (player.soundPlayer.HasAudio)
            {
                if (Convert.ToInt32(StopTimeInput.Text) < Convert.ToInt32(StartTimeInput.Text))
                {
                    throw new ArgumentOutOfRangeException("Stop time has to be bigger than starttime");
                }
                TimeSpan startTime = StartTimeInput.Text.Length > 0 ? TimeSpan.FromSeconds(Convert.ToInt32(StartTimeInput.Text)) : TimeSpan.FromSeconds(0);
                TimeSpan stopTime = StopTimeInput.Text.Length > 0 ? TimeSpan.FromSeconds(Convert.ToInt32(StopTimeInput.Text)) : TimeSpan.FromSeconds(0);
                player.PlayPause(startTime, stopTime);
            }
            else
            {
                MessageBox.Show("No song selected");
            }
        }

        private void SongSettingsUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
