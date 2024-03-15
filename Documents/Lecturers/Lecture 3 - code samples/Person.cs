using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndPlinq
{
    class Person
    {
        public String Name { get; set; }
        public int Age { get; set; }

        public Person(String Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
    }
}
