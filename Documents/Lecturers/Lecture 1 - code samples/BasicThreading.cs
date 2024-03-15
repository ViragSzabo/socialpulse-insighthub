using System;
using System.Threading;

namespace ThreadingHC1
{
    class BasicThreading
    {
        const string WELCOME_MESSAGE = "###Basic threading example###";
        const string PRESS_KEY = "Press any key to start...";
        const string DONE_MESSAGE = "Done with example, press any key to return to the main menu...";
        public BasicThreading()
        {
            Console.Clear();
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(PRESS_KEY);
            Console.ReadLine();

            //Initiate 2 threads
            //Thread t1 is a thread without a parameter
            //Thread t2 is a thread with parameter, it is being called via a LAMBDA expression (Basically a method within a parameter)
            Thread t1 = new Thread(SomeWork);
            Thread t2 = new Thread(() => SomeWork("SomeDataViaParameter"));

            //Start both threads after eachother
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
        /// Just some random work
        /// </summary>
        private void SomeWork()
        {
            Console.WriteLine("Thread 1 started. Doing very intensive work...");
            Thread.Sleep(5000);
            Console.WriteLine("Thread 1 stopped and done with very intensive work");
        }

        /// <summary>
        /// Just some random work with a parameter
        /// </summary>
        /// <param name="param">Just a random string</param>
        private void SomeWork(string param)
        {
            Console.WriteLine("Thread 2 started. Doing very intensive work with a parameter...");
            Thread.Sleep(4000);
            Console.WriteLine("Thread 2 stopped and done with very intensive work. The parameter was: {0}", param);
        }
    }
}
