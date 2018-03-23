using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.AccessControl;

namespace PrConSem
{
    class ProducerConsumerSemaphore
    {
        static int size = 20;
        private int items = 0;
        const string name_SemFree = "sem_free";
        const string name_SemFull = "sem_full";
        private Semaphore SemFree = new Semaphore(0, size, name_SemFree);
        private Semaphore SemFull = new Semaphore(size, size, name_SemFull);
        private Queue<int> my_queue = new Queue<int>();


        public void Produce()
        {
            while (items < size)
            {
                items++;

                //down(SemFree)
                //SemFree.WaitOne();
                //folosesc wait si release

                Semaphore.OpenExisting(name_SemFree, SemaphoreRights.Synchronize);
                //SemaphoreRights.Synchronize permite thread-urilor sa intre in sem, asemanator cu wait

                my_queue.Enqueue(items);
                Console.WriteLine("Produce {0}", items);


                ///// up(SemFull) /////              
                //SemFull.Release();

                Semaphore.OpenExisting(name_SemFull, SemaphoreRights.Modify);
                //Permite thread-urilor sa apeleze metoda Release

                /*try
                {
                    SemFree.Release();
                }
                catch { }*/

            }
        }

        public void Consume()
        {

            //down(SemFull)
            // SemFull.WaitOne();
            while (true)
            {
                while (items < 1)
                {
                    Semaphore.OpenExisting(name_SemFree, SemaphoreRights.Synchronize);
                    Semaphore.OpenExisting(name_SemFree, SemaphoreRights.Modify);
                }
                Semaphore.OpenExisting(name_SemFull, SemaphoreRights.Synchronize);
                //asemanator cu wait

                items = my_queue.Dequeue();
                try
                {
                    my_queue.Enqueue(items);
                }
                catch { } 


  
                Semaphore.OpenExisting(name_SemFree, SemaphoreRights.Modify); //asemanator cu release
                Console.WriteLine("Consume {0}", items);
                items--;
                //Console.WriteLine("Consume {0}", items);

                /* try
                 {
                     SemFull.Release();

                 }
                 catch { }*/
            }

        }
    }
}
