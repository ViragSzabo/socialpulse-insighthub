using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadingHC1
{
    class ThreadPoolSample
    {
        const string WELCOME_MESSAGE = "###Threadpool example###";
        const string PRESS_KEY = "Press any key to start...";
        const string DONE_MESSAGE = "Done with example, press any key to return to the main menu...";
        const int AMOUNT_O_WORK = 30;
        public ThreadPoolSample()
        {
            Console.Clear();
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(PRESS_KEY);
            Console.ReadLine();

            //Get al the number of available threads
            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("AmountOfWorker: {0}, AmountOfCompletion: {1}", workerThreads, completionPortThreads);

            //Dump work into threadpool
            //The threadpool will manage everything
            for (int i = 0; i < AMOUNT_O_WORK; i++)
            {
                ThreadPool.QueueUserWorkItem(SomeWork, i);
            }
            
            Console.WriteLine(DONE_MESSAGE);
            Console.ReadLine();
        }

        /// <summary>
        /// Just some random work
        /// </summary>
        /// <param name="threadId">An object with the threadId</param>
        private void SomeWork(object threadId)
        {
            Console.WriteLine("Thread {0} started. Is this a Threadpool thread? {1}. Doing very intensive work...", threadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(5000);
            Console.WriteLine("Thread {0} stopped and done with very intensive work", threadId);
        }
    }
}
