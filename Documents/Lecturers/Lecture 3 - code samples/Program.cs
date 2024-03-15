using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqAndPlinq
{
    class Program
    {
        static System.Diagnostics.Stopwatch Sw = new System.Diagnostics.Stopwatch();//Stopwatch for diagnostics
        const int SIZE = 2000000;
        const int SEED = 1234;
        const bool PRINT = true; //Printing for (Parallel)LinqExampleInt. Set to false for improved performance (but no printing)
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the (P)LINQ example program! Press Enter to start...");
            Console.ReadLine();

            #region LINQ with int
            Console.WriteLine("Starting LINQ example with int");
            Sw.Start();
            LinqExampleInt(); //Start LinqExampleInt
            Sw.Stop();
            Console.WriteLine("Time spent: {0}ms |Press any key to continue...", Sw.ElapsedMilliseconds);
            Sw.Reset();
            Console.ReadLine();
            #endregion
           
            #region PLINQ with int
            Console.WriteLine("Starting PLINQ example with int");           
            ParallelLinqExampleInt(); //Start PLinqExampleInt
            Console.ReadLine();
            #endregion
           
            #region LINQ with object
            Console.WriteLine("Starting LINQ example with object");
            Sw.Start();
            LinqExampleObject(); //Start LinqExampleObject
            Sw.Stop();
            Console.WriteLine("Time spent: {0}ms |Press Enter to continue...", Sw.ElapsedMilliseconds);
            Sw.Reset();
            Console.ReadLine();
            #endregion
            
            #region PLINQ with object
            Console.WriteLine("Starting PLINQ example with object");
            ParallelLinqExampleObject(); //Start PLinqExampleObject
            Console.ReadLine();
            #endregion

            #region PLINQ Ordered/unordered
            Console.WriteLine("Starting Ordered/unordered example");
            OrderedPlinq();
            Console.WriteLine("Done. Press enter to exit...");
            Console.ReadLine();
            #endregion

           
        }

        static void LinqExampleInt()
        {
            //Create rndgenerator
            Random rnd = new Random(SEED);
            //Create fixed collection
            int[] collection = new int[SIZE];
            //Fill with random numbers
            for (int i = 0; i < collection.GetLength(0); i++)
            {
                collection[i] = rnd.Next(0, 40);
            }

            //Create the query
            IEnumerable<int> linqQuery =    from element in collection //For every var now named  "Element" in the collection "collection"
                                            where element > 10 && element < 20 //With these restrictions (10 < element < 20
                                            select element; //Select the element that complies
            
            //Small counter
            int amountOElements = 0;

            //Activate the query
            foreach (int item in linqQuery)
            {
                if (PRINT)
                {
                    Console.WriteLine("Found element {0}! We now have {1} element(s)", item, amountOElements);
                    amountOElements++;
                }

            }
        }

        static void LinqExampleObject()
        {
            List<Person> Persons = new List<Person>
            {
                new Person("Slavic", 34),
                new Person("Cabal", 10000),
                new Person("Eva", 5000),
                new Person("Kane", 00),
                new Person("Seth", 50),
                new Person("Havoc", 28)
            };

            //Create the query
            IEnumerable<string> linqQuery = from element in Persons //For every var now named  "element" in the collection "collection"
                                            where element.Name.Contains("C") || element.Name.Contains("c") //With these restriction
                                            select element.Name; //Select the element that complies

            // Linq method
            //List<Person> personList = Persons.Where(element => element.Name.Contains("C") || element.Name.Contains("c")).ToList();

            //Activate the query
            foreach (string item in linqQuery)
            {
                Console.WriteLine("Found: {0}", item);
            }
        }
        
        static void ParallelLinqExampleInt()
        {
            //Create rndgenerator
            Random rnd = new Random(SEED);
            //Create fixed collection
            int[] collection = new int[SIZE];
            //Fill with random numbers
            for (int i = 0; i < collection.GetLength(0); i++)
            {
                collection[i] = rnd.Next(0, 40);
            }

            //Create the query
            ParallelQuery<int> linqQuery =  from element in collection.AsParallel()//.WithDegreeOfParallelism(2) //For every var now named  "Element" in the collection "collection" as a parallel operation
                                            where element > 10 && element < 20//With these restrictions (10 < element < 20
                                            select element; //Select the element that complies

            //Small counter
            int amountOElements = 0;
            Console.WriteLine("ForAll starts after pressing enter...");
            Console.ReadLine();
            Sw.Start();

            //Activate the query
            linqQuery.ForAll(item => {
                if (PRINT)
                {
                    amountOElements++;
                    Console.WriteLine("Found element {0}! We now have {1} element(s)", item, amountOElements);
                }
            }) ;

            Sw.Stop();
            Console.WriteLine("Time spent: {0}ms.", Sw.ElapsedMilliseconds);
            Sw.Reset();
            amountOElements = 0;
            Console.WriteLine("ForEach starts after pressing a Enter...");
            Console.ReadLine();
            Sw.Start();

            //Activate the query
            foreach (int item in linqQuery)
            {
                if (PRINT)
                {
                    amountOElements++;
                    Console.WriteLine("Found element {0}! We now have {1} element(s)", item, amountOElements);
                }
            }

            Sw.Stop();
            Console.WriteLine("Time spent: {0}ms.", Sw.ElapsedMilliseconds);
            Sw.Reset();
        }

        static void ParallelLinqExampleObject()
        {
            List<Person> Persons = new List<Person>
            {
                new Person("Slavic", 34),
                new Person("Cabal", 10000),
                new Person("Eva", 5000),
                new Person("Kane", 00),
                new Person("Seth", 50),
                new Person("Havoc", 28)
            };

            //Create the query
            ParallelQuery<string> linqQuery = from element in Persons.AsParallel()//For every var now named  "element" in the collection "collection"
                                            where element.Name.Contains("C") || element.Name.Contains("c") //With these restriction
                                            select element.Name; //Select the element that complies

            // Linq method
            //List<Person> personList = Persons.Where(p => p.Name.Contains("C") || p.Name.Contains("c")).ToList();

            //Activate the query
            Console.WriteLine("ForAll starts after pressing Enter...");
            Console.ReadLine();
            Sw.Start();
            linqQuery.ForAll(e => { Console.WriteLine("Found: {0}",e); });
            Sw.Stop();
            Console.WriteLine("Time spent: {0}ms.", Sw.ElapsedMilliseconds);
            Sw.Reset();


            Console.WriteLine("ForEach starts after pressing Enter...");
            Console.ReadLine();
            Sw.Start();
            foreach (string item in linqQuery)
            {
                Console.WriteLine("Found: {0}", item);
            }
            Sw.Stop();
            Console.WriteLine("Time spent: {0}ms.", Sw.ElapsedMilliseconds);
            Sw.Reset();
        }

        static void OrderedPlinq()
        {
            IEnumerable<int> sequence = Enumerable.Range(1, 100000);
            List<int> evenNumbers = sequence.AsParallel()
                                      .AsOrdered()
                                      .Where(x => x % 2 == 0)
                                      .ToList();
            foreach (int item in evenNumbers)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Unordered starts after pressing Enter...");
            Console.ReadLine();
            List<int> evenNumbersUnordered = sequence.AsParallel()
                                      .Where(x => x % 2 == 0)
                                      .ToList();
            foreach (int item in evenNumbersUnordered)
            {
                Console.WriteLine(item);
            }
            
        }
    }    
}