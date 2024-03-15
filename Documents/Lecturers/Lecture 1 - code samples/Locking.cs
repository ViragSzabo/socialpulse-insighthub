using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadingHC1
{
    class Locking
    {
        const string WELCOME_MESSAGE = "###Locking/MUTEX example###";
        const string PRESS_KEY = "Press any key to start...";
        const string DONE_MESSAGE = "Done with example, press any key to return to the main menu...";
        const string UNSAFE_START = "Starting unsafe threads";
        const string DONE_MIDWAY_MESSAGE = "Done with unsafe threads, press any key to continue...";
        const string SAFE_START = "Starting safe threads";

        /// <summary>
        /// Shared answer integer
        /// </summary>
        int ans;

        /// <summary>
        /// An object for use with the locker
        /// </summary>
        Object locker = new object();
        public Locking()
        {
            this.ans = 0;
            Console.Clear();
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(PRESS_KEY);
            Console.ReadLine();

            Console.WriteLine(UNSAFE_START);
            //Initiate 2 threads with the same method to execute
            Thread t1 = new Thread(Unsafe);
            Thread t2 = new Thread(Unsafe);
            //Start the threads
            t1.Start();
            t2.Start();
            //Wait for both threads to finish before continueing with the next lines of code
            //Comment these lines to see the difference!
            t1.Join();
            t2.Join();

            Console.WriteLine(DONE_MIDWAY_MESSAGE);
            Console.ReadLine();

            Console.WriteLine(SAFE_START);
            //Initiate 2 threads with the same method to execute
            t1 = new Thread(Safe);
            t2 = new Thread(Safe);
            //Start the threads
            t1.Start();
            t2.Start();
            //Wait for both threads to finish before continueing with the next lines of code
            //Comment these lines to see the difference!
            t1.Join();
            t2.Join();

            Console.WriteLine(DONE_MESSAGE);
            Console.ReadLine();
        }

        /// <summary>
        /// Unsafe method in multithreading with shared resource
        /// </summary>
        private void Unsafe()
        {
            for (int x = 0; x < 50; x++)
            {
                //this.ans can be accessed by multiple threads thus this is an unsafe operation in multithreading
                this.ans = this.ans + 1;
                Console.WriteLine("Ans equals: {0}", this.ans);
                this.ans = this.ans - 1;
                Console.WriteLine("Ans equals: {0}", this.ans);
            }
        }

        /// <summary>
        /// Safe method in multithreading with shared resource
        /// </summary>
        private void Safe()
        {
            for (int x = 0; x < 50; x++)
            {
                //Critical section -> Lock the locker object indicating that a thread is in the critical section
                //If another thread wants to enter, it will have to wait until the lock is released
                lock (this.locker)
                {
                    this.ans = this.ans + 1;
                    Console.WriteLine("Ans equals: {0}", this.ans);
                    this.ans = this.ans - 1;
                    Console.WriteLine("Ans equals: {0}", this.ans);
                }
                //Lock is now released
            }

        }
    }
}
