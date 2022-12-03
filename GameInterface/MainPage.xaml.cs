using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using GameLibrary;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.ApplicationModel.Core;
using Windows.Storage.Pickers;
using Windows.Storage;




// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

/* UWP Game Template
 * Created By: Luis Marquez
 */


namespace GameInterface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        List<GamePiece> enemies = new List<GamePiece>();
        DispatcherTimer timer = new DispatcherTimer();
        MediaPlayer player;
        int totalScore = 0;
        int totalEnemies = 0;
        string nameScore = "";
        string scoreList = "List of Winners";
        int time = 0;
        bool gameOver = false;


        //private static GamePiece player;
        private static GamePiece dot;
        private static GamePiece ship;
        //Create Enemies "The Marcianos" (The Aliens)
        private static GamePiece greenAlien;
        private static GamePiece pinkAlien;
        private static GamePiece purpleAlien;
        private static GamePiece greenAlien2;
        private static GamePiece pinkAlien2;
        private static GamePiece purpleAlien2;
        private static GamePiece greenAlien3;
        private static GamePiece pinkAlien3;
        private static GamePiece purpleAlien3;
        private static GamePiece greenAlien4;
        private static GamePiece pinkAlien4;
        private static GamePiece purpleAlien4;
        private static GamePiece greenAlien5;
        private static GamePiece pinkAlien5;
        private static GamePiece purpleAlien5;
        private static GamePiece greenAlien6;
        private static GamePiece pinkAlien6;




        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            PlayMusic();
            CreateEnemies();
            totalEnemies = enemies.Count();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_tick;
            timer.Start();
            readScores();
            player = new MediaPlayer();
            //player = CreatePiece("player", 100, 50, 50);       //create a GamePiece object associated with the pac-man image
            ship = CreatePiece("awesomeShip", 150, 800, 800);
   

        }



        private GamePiece CreatePiece(string imgSrc, int size, int left, int top)                 //constructor creates a piece and its associated image
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{imgSrc}.png"));
            img.Width = size;
            img.Height = size;
            img.Name = $"img{imgSrc}";
            img.Margin = new Thickness(left, top, 0, 0);
            img.VerticalAlignment = VerticalAlignment.Top;
            img.HorizontalAlignment = HorizontalAlignment.Left;

            gridMain.Children.Add(img);

            return new GamePiece(img);
        }

        private async void CoreWindow_KeyDown(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            //Gets record of the total Enemies created

            enemiesLeft.Text = "Enemies Left: " + totalEnemies.ToString();

            
            //Calculate new location for the player character
            ship.MovePlayer(e.VirtualKey);
            
            
            if (e.VirtualKey == Windows.System.VirtualKey.Space)
            {
                CreateShipBullet(e.VirtualKey);
                dot.MoveBullet();
            }


            //Check for collisions between player and collectible
            //Note: this looks for identical locations of the two objects. To be more precise, you can write a method to measure distances
            if (dot == null)
            {
                return;
            }

            foreach(GamePiece x in enemies.ToList())
            {
                if (enemies.Contains(x))
                {
                    //int enemyTopStart = int.Parse(x.Location.Top.ToString());
                    //int enemyTopEnd = int.Parse(x.Location.Top.ToString())+15;

                    x.MoveAlien();

                    //Tried to make it more exact, but I couldn't find a way to eliminate the alien once is touched by the bullet.
                    
                  
                }
                int enemyStart = int.Parse(x.Location.Left.ToString());
                int enemyEnd = int.Parse(x.Location.Left.ToString()) + 15;
                if (Enumerable.Range(enemyStart, enemyEnd - enemyStart + 1).Contains(int.Parse(dot.Location.Top.ToString())))
                {

                    x.RemoveItem();
                    enemies.Remove(x);
                    totalScore += 1000;
                    txtScore.Text = $"SCORE: {totalScore}";
                    totalEnemies -= 1;
                    break;
                }

            }

            //Message once the game has been completed
            if(totalEnemies < 1)
            {
                showGameOver("You saved the world from THE MARCIANOS (the aliens)\nCONGRATULATIONS! ");
            }

            //Restart the project when enter is pressed.

            if(e.VirtualKey == Windows.System.VirtualKey.Enter && gameOver == true)
            {
                await CoreApplication.RequestRestartAsync("Application Restart");
            }



        }


        //Method to create new ship bullets
        public void CreateShipBullet(Windows.System.VirtualKey direction)
        {
            switch (direction)
            {
                case Windows.System.VirtualKey.Space:
                    dot = CreatePiece("dot", 15, int.Parse((ship.Location.Left + 68).ToString()), int.Parse((ship.Location.Top - 10).ToString()));                      
                    break;
            }
        }

        //Creating Enemies to show on screen
        public void CreateEnemies()
        {


            greenAlien = CreatePiece("greenAlien", 100, 700, 150);
            pinkAlien = CreatePiece("pinkAlien", 100, 800, 150);
            purpleAlien = CreatePiece("purpleAlien", 100, 900, 150);
            greenAlien2 = CreatePiece("greenAlien", 100, 1000, 150);
            pinkAlien2 = CreatePiece("pinkAlien", 100, 1100, 150);
            purpleAlien2 = CreatePiece("purpleAlien", 100, 600, 250);
            greenAlien3 = CreatePiece("greenAlien", 100, 700, 250);
            pinkAlien3 = CreatePiece("pinkAlien", 100, 800, 250);
            purpleAlien3 = CreatePiece("purpleAlien", 100, 900, 250);
            greenAlien4 = CreatePiece("greenAlien", 100, 1000, 250);
            pinkAlien4 = CreatePiece("pinkAlien", 100, 1100, 250);
            purpleAlien4 = CreatePiece("purpleAlien", 100, 1200, 250);
            greenAlien5 = CreatePiece("greenAlien", 100, 700, 350);
            pinkAlien5 = CreatePiece("pinkAlien", 100, 800, 350);
            purpleAlien5 = CreatePiece("purpleAlien",100, 900, 350);
            greenAlien6 = CreatePiece("greenAlien", 100, 1000, 350);
            pinkAlien6 = CreatePiece("pinkAlien", 100, 1100, 350);

            enemies.Add(pinkAlien);
            enemies.Add(greenAlien);
            enemies.Add(purpleAlien);
            enemies.Add(pinkAlien2);
            enemies.Add(greenAlien2);
            enemies.Add(purpleAlien2);
            enemies.Add(pinkAlien3);
            enemies.Add(greenAlien3);
            enemies.Add(purpleAlien3);
            enemies.Add(pinkAlien4);
            enemies.Add(greenAlien4);
            enemies.Add(purpleAlien4);
            enemies.Add(pinkAlien5);
            enemies.Add(greenAlien5);
            enemies.Add(purpleAlien5);
            enemies.Add(pinkAlien6);
            enemies.Add(greenAlien6);

        }

        //Play the cool tune!
        public async void PlayMusic()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("TITO RODRIGUEZ_los_marcianos.mp3");

            player.AutoPlay = true;
            player.Source = MediaSource.CreateFromStorageFile(file);
            player.Play();
        }


        public void showGameOver(string msg)
        {
            
            player.Pause();
            timer.Stop();
            popUp.IsOpen = true;
            gameOver = true;
            enemiesLeft.Text = "GAME OVER\n" + msg + "\nPress Enter to Play again";
        }



        public void timer_tick(object sender, object e)
        {
            time = time + 1;
            txtTimer.Text = $"Timer: {time.ToString()} Seconds";
        }

        public async void createHighScore()
        {
            nameScore = txtName.Text;
            FolderPicker fp = new FolderPicker();
            fp.SuggestedStartLocation = PickerLocationId.Desktop;
            fp.FileTypeFilter.Add("*");

            

            StorageFolder folder = await fp.PickSingleFolderAsync();

            if (folder == null) return;

            StorageFile file = await folder.CreateFileAsync("HighScores.txt", CreationCollisionOption.OpenIfExists);


            //write to a file using stream with datawriter
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputStream = stream.GetOutputStreamAt(stream.Size))
            {
                using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                {
                    dataWriter.WriteString($"\nName: {nameScore}, Score: {totalScore}, Time: {time} seconds");
                    await dataWriter.StoreAsync();
                    await outputStream.FlushAsync();
                }
            }
            stream.Dispose();       //get rid of the stream when we are finished with it

        }

        public async void readScores()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".txt");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file == null) return;

            var lines = await Windows.Storage.FileIO.ReadLinesAsync(file);
            foreach (string line in lines)
            {
                scoreList += $"\n{line}";
            }
            winnersTxtLabel.Text = scoreList;

        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            createHighScore();
            txtName.Text = "";
            popUp.IsOpen = true;
            
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
             CoreApplication.Exit();
        }
    }
}
