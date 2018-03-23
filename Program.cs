using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PrConSem
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumerSemaphore prod_cons = new ProducerConsumerSemaphore();
            Thread producer = new Thread(new ThreadStart(prod_cons.Produce));
            Thread consumer = new Thread(new ThreadStart(prod_cons.Consume));

            producer.Start();
            consumer.Start();

            producer.Join();
            consumer.Join();

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey(false);

        }
    }
}
