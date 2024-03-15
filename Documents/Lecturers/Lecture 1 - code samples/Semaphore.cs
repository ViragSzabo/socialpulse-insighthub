using System;
using System.Threading;

namespace ThreadingHC1
{
    class Semaphore
    {
        const string WELCOME_MESSAGE = "###Semaphore example###";
        const string PRESS_KEY = "Press any key to start...";
        const string DONE_MESSAGE = "Done with example, press any key to return to the main menu...";

        //Create an array for all the threads so we can join them with the main thread later
        Thread[] threadList = new Thread[20];
        //Create semaphore
        SemaphoreSlim bouncer = new SemaphoreSlim(5);
        public Semaphore(bool funky = false)
        {
            Console.Clear();
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(PRESS_KEY);
            Console.ReadLine();
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

            if (funky)
            {
                wplayer.URL = "night.mp3";
                wplayer.controls.play();
            }

            for (int i = 0; i < threadList.GetLength(0); i++)
            {
                //We use localI to prevent the delay of creating thread to interfere with the treadname
                int localI = i;
                //Create the thread with a lambda
                this.threadList[i] = new Thread(() => NightFever("Thread" + localI));
                //Start the thread
                this.threadList[i].Start();
                //Simulate a list of people, so we can see the "queue" grow
                Thread.Sleep(100);
            }
            
            //Make sure we join all the thread with the main thread
            for (int i = 0; i < threadList.GetLength(0); i++)
            {
                this.threadList[i].Join();
            }

            Console.WriteLine("The Disco is closed for the night");
            Console.WriteLine(DONE_MESSAGE);
            Console.ReadLine();
        }

        private void NightFever(String threadName)
        {
            Console.WriteLine("The person with the Threadname {0} wants to enter the Disco", threadName);
            //Ask the semaphore to enter. If there is no room -> wait untill room is available
            this.bouncer.Wait();

            #region Critical section of the semaphore example (A Disco)
            Console.WriteLine("The person with the Threadname {0} has entered the Disco. \nTime to party!", threadName);
            Thread.Sleep(5000);
            Console.WriteLine("Threadname {0} is still dancing!", threadName);
            Thread.Sleep(5000);
            Console.WriteLine("The person with the Threadname {0} is done for the night and left the Disco.", threadName);
            #endregion

            //Notify the semaphore that there is a space available
            this.bouncer.Release();
        }
    }
}
