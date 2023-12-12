using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Asynchronous
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] strMsg = new string[5];

        private async void button1_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();

            //var nums = Enumerable.Range(0, 5);
            //foreach (var  x in nums)
            //{
            //    Task t = Task.Run(() =>
            //    {
            //        heavyTask();
            //        strMsg[x] = $"Task {x + 1:00} finished\r\n";
            //        Console.WriteLine(strMsg[x]);
            //        //this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
            //    });
            //}

            for (int i = 0; i < 5; i++)
            {
                var x = i;
                Task t = Task.Run(() =>
                {
                    heavyTask(x);
                    strMsg[x] = $"Task {x + 1:00} finished\r\n";
                    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
                });
                tasks.Add(t);
                var t2 = Task.Run(() =>
                  {
                      var ret = heavyTask2(x);
                      var Msg = $"Task {x + 1:00}-2 finished {ret} sec\r\n";
                      this.Invoke((MethodInvoker)(() => textBox1.AppendText(Msg)));
                  });
                tasks.Add(t2);
            }
            var tt = Task.WhenAll(tasks);
            try
            {
                await tt;
            }
            catch (Exception)
            {
                if (tt.Exception is AggregateException)
                {

                    // このコードでは動きません。
                    //foreach (var t in tasks)
                    //{
                    //    var ex = t.Exception;
                    //    textBox1.AppendText($"{ex.Message}");
                    //}

                    var ers = tt.Exception;

                    foreach (var er in ers.Flatten().InnerExceptions)
                    {
                        textBox1.AppendText($"{er.Message}\r\n");
                        textBox1.AppendText($"{er.TargetSite.Name}\r\n\r\n");
                    }

                }
            }
            Console.WriteLine("All task fin");
            textBox1.AppendText("All Proccess Finished\r\n");

        }

        private void heavyTask(int i)
        {
            try
            {
                Thread.Sleep(1000);

                if (i == -1 || i == 6)
                {
                    var e = new Exception($"Task:{i + 1:00} Error Occured!");
                    throw e;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private int heavyTask2(int i)
        {
            var seed = DateTime.Now.Millisecond;
            var sec = new Random(seed).Next(1, 10);

            try
            {
                Thread.Sleep(500 * sec);
                Raise_Error_0(i);
                Raise_Error_1(i);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sec;
        }

        private void Raise_Error_0(int i)
        {
            // Try ～ Catch によるエラートラップあり
            try
            {
                if (i == 3)
                {
                    var e = new Exception($"Task:{i + 1:00} Error Occured!");
                    throw e;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.TargetSite.Name}");
                throw ex;
            }
        }

        private void Raise_Error_1(int i)
        {
            // エラートラップなし
            if (i == 4)
            {
                var e = new Exception($"Task:{i + 1:00} Error Occured!");
                throw e;
            }
        }


        private void done_heavyTasks()
        {
            List<Task> tasks = new List<Task>();

            var nums = Enumerable.Range(0, 5);
            foreach (var x in nums)
            {
                Task t = Task.Run(() =>
                {
                    heavyTask(x);
                    strMsg[x] = $"Task {x + 1:00} finished\r\n";
                    Console.WriteLine(strMsg[x]);
                    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
                });
            }
            Task tt = Task.WhenAll(tasks);
            try
            {
                tt.Wait();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
