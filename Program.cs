using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using System.Media;

//
// Application Name: DrunkPC
// Description: Application that generates erratic mouse and keyboard input 
//              and generates fakes sound and dialogs to confuse the user
//

namespace DrunkPC
{
    class Program
    {
        // using underscore to visually indicate a global variable
        public static Random _random = new Random();
        public static int _startupDelaySeconds = 5;
        public static int _durationLoopSeconds = 10;
        public static int _howManyTimes = 3;

        /// <summary>
        /// Prank Application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _durationLoopSeconds = Convert.ToInt32(args[1]);
                _howManyTimes = Convert.ToInt32(args[2]);
            }   

            //Create all Threads
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            #region IniciatePrank

            DateTime time = DateTime.Now.AddSeconds(_startupDelaySeconds);
            while (time > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
            //Start all Threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            for (int i = 0; i <= _howManyTimes; i++)
            {
                DateTime temp = DateTime.Now.AddSeconds(_durationLoopSeconds);
                while (temp > DateTime.Now)
                {                
                    Thread.Sleep(1000);
                }
                drunkMouseThread.Suspend();
                drunkKeyboardThread.Suspend();
                drunkSoundThread.Suspend();
                drunkPopupThread.Suspend();

                temp = DateTime.Now.AddSeconds(_startupDelaySeconds);
                while (temp > DateTime.Now)
                {
                    Thread.Sleep(1000);
                }
                //Start all Threads
                drunkMouseThread.Resume();
                drunkKeyboardThread.Resume();
                drunkSoundThread.Resume();
                drunkPopupThread.Resume();
            }

            time = DateTime.Now.AddSeconds(_durationLoopSeconds);
            while (time > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
            //Finaliza all Threads
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
            #endregion
        }

        #region Thread Functions
        static void DrunkMouseThread() 
        {
            int moveX = 0;
            int moveY = 0;
            while (true)
            {
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        static void DrunkKeyboardThread()
        {
            while (true)
            {
                char key = (char)(_random.Next(25) + 65 );
                if (_random.Next(2) == 0)
                {
                    key = char.ToLower(key);
                }
                SendKeys.SendWait(key.ToString());
                Thread.Sleep(_random.Next(300));
            }
        }

        static void DrunkSoundThread()
        {
            while (true)
            {
                if (_random.Next(100) > 80 )
                {
                    switch (_random.Next(5))
	                {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
		                default:
                            break;
	                }
                }
                Thread.Sleep(_random.Next(1000));
            }
        }

        static void DrunkPopupThread()
        {
            while (true)
            {
                if (_random.Next(100) > 90)
                {
                    MessageBox.Show(
                        "Internet Explorer has stopped working",
                        "Internet Explorer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}
