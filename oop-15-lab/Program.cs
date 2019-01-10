using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;


namespace oop_15_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            //DoTask1();
            //DoTask2();
            Console.WriteLine("Введите число: ");
            try
            {
                MaxNumber = Convert.ToInt32(Console.ReadLine());
            }
            catch { }
            //DoTask3();


            //DoTask4a();
            //DoTask4b_i();
            //DoTask4b_ii();
            //DoTask5();
            DoTaskDop();




            #region Task 1
            //Определите и выведите на консоль / в файл все запущенные процессы:id, 
            //имя, приоритет, время запуска, текущее состояние, сколько всего времени использовал процессор и т.д.
            void DoTask1()
            {
                foreach (var i in Process.GetProcesses())
                {
                    try
                    {
                        Console.WriteLine("ID: " + i.Id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ID: " + e.Message);
                    }
                    try
                    {
                        Console.WriteLine("Приоритет: " + i.BasePriority);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Приоритет: " + e.Message);
                    }
                    try
                    {
                        Console.WriteLine("Время запуска: " + i.StartTime);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Время запуска: " + e.Message);
                    }
                    try
                    {
                        Console.WriteLine("Сколько всего времени использовался процесс: " + i.UserProcessorTime);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Сколько всего времени использовался процесс: " + e.Message);
                    }
                    try
                    {
                        Console.WriteLine("Дискриптор процесса: " + i.Handle);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Дискриптор процесса: " + e.Message);
                    }

                    Console.WriteLine();

                }
            }
#endregion



            #region Task 2

            //Исследуйте текущий домен вашего приложения: имя, детали конфигурации, все сборки, загруженные в домен.
            void DoTask2()
            {
                AppDomain CurrentDomain = AppDomain.CurrentDomain;

                try
                {
                    Console.WriteLine("Имя домна: " + CurrentDomain.FriendlyName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Имя домна: " + e.Message);
                }
                try
                {
                    Console.WriteLine("Детали конфигурации: " + CurrentDomain.SetupInformation);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Детали конфигурации: " + e.Message);
                }

                try
                {
                    Console.WriteLine("Сборки: ");
                    foreach (var i in CurrentDomain.GetAssemblies())
                    {
                        Console.WriteLine(i);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine();
                Console.Read();
                Console.Clear();

                //    Создайте новый домен.
                AppDomain NewDomain = AppDomain.CreateDomain("DomainForTask2");

                NewDomain.AssemblyLoad += AssemblyLoad;
                NewDomain.DomainUnload += DomainUnload;

                Console.WriteLine("Домен: " + NewDomain.FriendlyName);               

                NewDomain.Load("oop-15-lab");

                Console.WriteLine("Сборки: ");
                foreach (var i in NewDomain.GetAssemblies())
                {
                    Console.WriteLine(i);
                }
                //    Выгрузите домен.
                AppDomain.Unload(NewDomain);
            }
            #endregion



            #region Task 3
            void DoTask3()
            {
                Thread thread = new Thread(Counter) { Name = "Counter" };

                thread.Start();
                thread.Suspend();
                Console.WriteLine("Имя потока: {0}", thread.Name);

                Console.WriteLine("Запущен ли поток: {0}", thread.IsAlive);
                Console.WriteLine("Приоритет потока: {0}", thread.Priority);
                Console.WriteLine("Статус потока: {0}", thread.ThreadState);

                // получаем домен приложения
                Console.WriteLine("Домен приложения: {0}", Thread.GetDomain().FriendlyName);

                thread.Resume();
                //thread.Abort();
                #endregion
                Console.WriteLine();
                
            }
            #region Task 4

            //Создайте два потока.Первый выводит  четные числа, второй нечетные до n и записывают их  в общий файл и на консоль. 
            //    Скорость расчета чисел у потоков – разная.
            //        a.Поменяйте приоритет одного из потоков.
            //        b.Используя средства синхронизации организуйте работу потоков, таким образом, чтобы 
            //            i. выводились сначала четные, потом нечетные числа 
            //            ii. последовательно выводились одно четное, другое нечетное.

            

            #region Task 4 a
            void DoTask4a()
            {
                try
                {
                    Thread thread1 = new Thread(PrintEvenNumberTa) { Name = "First" };

                    Thread thread2 = new Thread(PrintOddNumberTa) { Name = "Second" };


                    Console.ForegroundColor = ConsoleColor.Green;
                    Print(thread1);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Print(thread2);
                    Console.ResetColor();

                    Console.WriteLine("\nЗадание а\n");
                    thread2.Priority = ThreadPriority.Highest;

                    thread1.Start();
                    thread2.Start();

                    Print(thread2);



                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            #endregion


            #region Task 4 b i
            void DoTask4b_i()
            {

                Thread thread1 = new Thread(PrintEvenNumberTb) { Name = "First" };

                Thread thread2 = new Thread(PrintOddNumberTb) { Name = "Second" };

                thread1.Start();
                thread2.Start();
            }
            #endregion

            #region Task 4 b ii
            void DoTask4b_ii()
            {
                Thread thread1 = new Thread(PrintEvenNumberTbii) { Name = "First" };

                Thread thread2 = new Thread(PrintOddNumberTbii) { Name = "Second" };

                thread1.Start();
                thread2.Start();
            }
            #endregion

            #endregion

            #region Task 5
            void DoTask5()
            {
                TimerCallback callback = new TimerCallback(PrintLouding);
                Timer timer = new Timer(callback, null, 0, 1000);
                Console.Read();

                void PrintLouding(object o)
                {


                    if (ColorCounter > 5)
                        ColorCounter = 0;
                    Console.ForegroundColor = ConsoleColor.Blue + ColorCounter;
                    Console.Clear();
                    Console.Write("Загрузка ");
                    for (int i = 0; i <= ColorCounter % 3; i++)
                    {
                        Console.Write(".");
                    }
                    ColorCounter++;
                }
            }
            #endregion

            #region Dop
            void DoTaskDop()
            {
                Thread car1 = new Thread(TakeGoods);
                Thread car2 = new Thread(TakeGoods);
                Thread car3 = new Thread(TakeGoods);

                car1.Start();
                car2.Start();
                car3.Start();
            }
            #endregion

        }



        static int ColorCounter = 0;
        static object loker = new object();
        static public void PrintEvenNumberTbii()
        {
            
                for (int i = 0; i <= MaxNumber; i++)
                {
                    if (i % 2 == 0)
                    {
                    Monitor.Enter(loker);
                    using (StreamWriter sw = new StreamWriter("Taskbii.txt", true))
                    {
                        sw.WriteLine(i);
                    }
                        Console.WriteLine(i);
                        Thread.Sleep(1500);
                    Monitor.Exit(loker);

                }
            }
            
            Console.Read();
        }

        static void PrintOddNumberTbii()
        {
            
                for (int i = 0; i <= MaxNumber; i++)
                {
                    if (i % 2 == 1)
                    {
                    Monitor.Enter(loker);
                    using (StreamWriter sw = new StreamWriter("Taskbii.txt", true))
                    {
                        sw.WriteLine(i);
                    }
                    Console.WriteLine(i);
                    Thread.Sleep(2000);
                    Monitor.Exit(loker);

                    }
                }
            
        }



        static public void PrintEvenNumberTb()
        {
            lock (loker)
            { 
                for (int i = 0; i <= MaxNumber; i++)
                {
                    if (i % 2 == 0)
                    {
                        using (StreamWriter sw = new StreamWriter("Taskbi.txt", true))
                        {
                            sw.WriteLine(i);
                        }
                        Console.WriteLine(i);
                        Thread.Sleep(1500);
                    }
                }
            }
            Console.Read();
        }

        static void PrintOddNumberTb()
        {
            lock (loker)
            {
                for (int i = 0; i <= MaxNumber; i++)
                {
                    if (i % 2 == 1)
                    {
                        using (StreamWriter sw = new StreamWriter("Taskbi.txt", true))
                        {
                            sw.WriteLine(i);
                        }
                        Console.WriteLine(i);
                        Thread.Sleep(2000);

                    }
                }
            }
        }

        static public void Print(Thread thread)
        {
            Console.WriteLine("Имя потока: {0}", thread.Name);

            Console.WriteLine("Запущен ли поток: {0}", thread.IsAlive);
            Console.WriteLine("Приоритет потока: {0}", thread.Priority);
            Console.WriteLine("Статус потока: {0}", thread.ThreadState);

            // получаем домен приложения
            Console.WriteLine("Домен приложения: {0}", Thread.GetDomain().FriendlyName);
        }
        public static int MaxNumber;

        static public void PrintEvenNumberTa()
        {
            for(int i=0;i<=MaxNumber;i++)
            {
                if(i%2==0)
                {
                    lock (loker)
                    {
                        using (StreamWriter sw = new StreamWriter("Taska.txt", true))
                        {
                            sw.WriteLine(i);
                        }
                    }
                        Console.WriteLine(i);
                        Thread.Sleep(1500);
                    
                }
            }
            Console.Read();
        }
        static void PrintOddNumberTa()
        {
            for (int i = 0; i <= MaxNumber; i++)
            {
                if (i % 2 == 1)
                {
                    lock (loker)
                    {
                        using (StreamWriter sw = new StreamWriter("Taska.txt", true))
                        {
                            sw.WriteLine(i);
                        }
                    }
                        Console.WriteLine(i);
                        Thread.Sleep(2000);
                    
                }
            }
        }
        
        private static void Counter()
        {
            try
            {
                int counter=0;
                //int MaxValue = Convert.ToInt32(Console.ReadLine());
                using (StreamWriter fs = new StreamWriter("Counter.txt"))
                {
                    for (int i = 2; i <= MaxNumber; i++)
                    {
                        counter = 0;
                        for(int j=(int)Math.Sqrt(i);j>1;j--)
                            if(i%j==0)
                            {
                                counter++;
                            }
                        if (counter == 0 )
                        {
                            Console.WriteLine(i);
                            fs.WriteLine(i);
                        }
                        Thread.Sleep(700);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private static void DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен выгружен из процесса");
        }

        private static void AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Сборка загружена");
        }

        static public Mutex mutex = new Mutex();

        static public int counterMutex = 1;

        public static void TakeGoods()
        {
            mutex.WaitOne();
            Console.ForegroundColor = ConsoleColor.Cyan + counterMutex;
            Console.WriteLine("Машина " + counterMutex + " Загружается...");
            Thread.Sleep(4000);
            using (StreamReader sr = new StreamReader("ДопМашины.txt", System.Text.Encoding.Default))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
            Console.WriteLine("Машина " + counterMutex + " Закончила...");
            counterMutex++;
            Thread.Sleep(1000);

            mutex.ReleaseMutex();
        }
    }
}
