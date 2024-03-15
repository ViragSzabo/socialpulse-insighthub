using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingHC2
{
    class TaskingSample
    {
        const string WELCOME_MESSAGE = "###Tasking example###";
        const string PRESS_KEY = "Press any key to start...";
        const string DONE_MESSAGE = "Done with example, press any key to return to the main menu...";
        public TaskingSample()
        {
            Console.Clear();
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(PRESS_KEY);
            Console.ReadLine();

            //Starting multiple tasks in multiple ways

            Task.Factory.StartNew(SomeWork);                //Starts immidiatly
            Task.Factory.StartNew(() => SomeWork());        //Starts immidiatly (Lambda direct)
            Task.Factory.StartNew(() => 
            {
                //Other methods or work can be done here aswell!
                Console.WriteLine("Hello");
                SomeWork();
                //Other methods or work can be done here aswell!

            });                                             //Starts immidiatly (Lambda Anonymous)

            Task t1 = new Task(SomeWork);
            Task t2 = new Task(() => SomeWork());           //Lambda Direct
            Task t3 = new Task(() =>
            {
                //Other methods or work can be done here aswell!
                SomeWork();
                //Other methods or work can be done here aswell!

            });                                             //Lambda Anonymous
            Task t4 = new Task(new Action(SomeWork));

            Console.ReadLine();

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            Console.WriteLine("");
            Console.WriteLine("Tasks started manually");    //This message can be shown before the tasks are started. This because it takes time to start them!
            Console.WriteLine(DONE_MESSAGE);
            Console.ReadLine();
            
        }

        private void SomeWork()
        {            
            int? id = Task.CurrentId;                       //int? means that it can be a null.
            //int x = null;                                 //This is illegal because int can't be null. (uncomment to see)
            Console.WriteLine("ID: {0} is starting.", id);
            Thread.Sleep(1000);
            Console.WriteLine("ID: {0} is done.", id);
        }
    }
}