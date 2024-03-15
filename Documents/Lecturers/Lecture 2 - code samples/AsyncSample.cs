using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ThreadingHC2
{
    class AsyncSample
    {
        const string WELCOME_MESSAGE = "###Async example###";
        const string PRESS_KEY = "Press any key to start...";
        const string DONE_MESSAGE = "Done with example, press any key to return to the main menu...";

        public AsyncSample()
        {
            Console.Clear();
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(PRESS_KEY);
            Console.ReadLine();
            AsyncMethodExample();               //This method runs async -> we wont wait for its completion (This is not possible in a constructor)
            AsyncMethodExample2();              //This method runs async -> we wont wait for its completion (This is not possible in a constructor)
            Console.WriteLine(DONE_MESSAGE);    //Because the above 2 methods run async, this message will show faster than intended...
            Console.ReadLine();
        }

        public async void AsyncMethodExample()  //'async' will tell the compiler that the 'await' operator can be used in this method
        {
            WebRequest request = WebRequest.Create("https://www.realtek.com/zh-tw/");
            Task<WebResponse> responseTask = request.GetResponseAsync(); //Wil run async

            //Do other stuff in the meantime
            Console.WriteLine("Waiting for a miracle...");
            Console.WriteLine("A miracle occurred!");
            //Or do nothing, our choice

            await responseTask; //We need the data from the responseTask. If it's not there yet, jump back to the caller so the main thread isn't blocked.
            Console.WriteLine("ContentLength: {0}", responseTask.Result.ContentLength);
        }

        public async void AsyncMethodExample2()
        {
            //Create an anonymous task
            Task<bool> t = new Task<bool>(() =>
            {
                for (int i = 0; i < 18000; i++)
                {
                    System.Diagnostics.Trace.Write(i);
                }
                return true;

            });

            //start the task
            t.Start();
            Console.WriteLine("Busy...");
            //We need the result of our task, so we wait here.
            await t;
            Console.WriteLine("Result is: {0}", t.Result.ToString());
        }
    }
}
