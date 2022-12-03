using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;


/* UWP Game Library
 * Written By: Luis Marquez
 */

namespace GameLibrary
{
    public class GamePiece
    {
        private Thickness objectMargins;            //represents the location of the piece on the game board
        private Image onScreen;
        DispatcherTimer gameTimer = new DispatcherTimer(); //Creates a timer which helps to the objects movement
        public Thickness Location                     //get access only - can not directly modify the location of the piece
        {
            get { return onScreen.Margin; }
        }

        //public GamePiece Comp { get; }

        public GamePiece(Image img)                 //constructor creates a piece and a reference to its associated image
        {                                           //use this to set up other GamePiece properties
            onScreen = img;
            objectMargins = img.Margin;
        }

        

        public bool MovePlayer(Windows.System.VirtualKey direction)   //calculate a new location for the piece, based on a key press
        {
            switch (direction)
            {
                //case Windows.System.VirtualKey.Up:
                //    objectMargins.Top -= 10;

                //    break;
                //case Windows.System.VirtualKey.Down:
                //    objectMargins.Top += 10;

                    //break;
                case Windows.System.VirtualKey.Left:
                    objectMargins.Left -= 15;

                    break;
                case Windows.System.VirtualKey.Right:
                    objectMargins.Left += 15;

                    break;

                default:
                    return false;
            }
            onScreen.Margin = objectMargins;
            return true;
        }


        //Method to move an object up. Is used for the bullet(dot)
        public void MoveBullet()
        {
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += Timer_Tick;
            gameTimer.Start();
        }
        
        // Method to initiate a timer once a bullet (dot) is shot. It dissapears once it reaches the top of the screen
        private void Timer_Tick(object sender, object e)
        {
            if (objectMargins.Top > 0)
            {
                objectMargins.Top -= 20;
                onScreen.Margin = objectMargins;
            }
            else
            {
                onScreen.Visibility = Visibility.Collapsed;
                gameTimer.Stop();
                RemoveItem();
            }
        }

        //Method to move the marcianos (Aliens)
        public void MoveAlien()
        {
            gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            gameTimer.Tick += alien_Timer_Tick;
            gameTimer.Start();
        }

        //Methid to initiate a timer to move the aliens
        private void alien_Timer_Tick(object sender, object e)
        {
            if (objectMargins.Left > 100)
            {
                //Makes the alien move left
                objectMargins.Left -= 2;
                onScreen.Margin = objectMargins;
            }
            else
            {
                objectMargins.Left += 1800;
                onScreen.Margin = objectMargins;
            }
        }


        //method to make dissapear the item shot
        public void RemoveItem()
        {
            onScreen.Visibility = Visibility.Collapsed;
        }


    }
}
