using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                    heavyTask();
                    strMsg[x] = $"Task {x + 1:00} finished\r\n";
                    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
                });
                tasks.Add(t);
            }
            await Task.WhenAll(tasks);
            //Task tt = Task.WhenAll(tasks);
            //tt.Wait();
            //for (int i = 0; i < 5; i++) 
            //{
            //    textBox1.AppendText(strMsg[i]);
            //}

            //Task t0 = Task.Run(() =>
            //{
            //    heavyTask();
            //    strMsg[0] = $"Task 01 finished\r\n";
            //    Console.WriteLine(strMsg[0]);
            //    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[0])));
            //});

            //Task t1 = Task.Run(() =>
            //{
            //    heavyTask();
            //    strMsg[1] = $"Task 02 finished\r\n";
            //    Console.WriteLine(strMsg[1]);
            //    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[1])));
            //});
            //Task t2 = Task.Run(() =>
            //{
            //    heavyTask();
            //    strMsg[2] = $"Task 03 finished\r\n";
            //    Console.WriteLine(strMsg[2]);
            //    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[2])));
            //});
            //Task t3 = Task.Run(() =>
            //{
            //    heavyTask();
            //    strMsg[3] = $"Task 04 finished\r\n";
            //    Console.WriteLine(strMsg[3]);
            //    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[3])));
            //});
            //Task t4 = Task.Run(() =>
            //{
            //    heavyTask();
            //    strMsg[4] = $"Task 05 finished\r\n";
            //    Console.WriteLine(strMsg[4]);
            //    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[4])));
            //});

            ////Task t = Task.Run(() => done_heavyTasks());
            ////await t;
            //await Task.WhenAll(t0, t1, t2, t3, t4);
            Console.WriteLine("All task fin");
            textBox1.AppendText("All Proccess Finished");

        }

        private void done_heavyTasks()
        {
            List<Task> tasks = new List<Task>();

            var nums = Enumerable.Range(0, 5);
            foreach (var x in nums)
            {
                Task t = Task.Run(() =>
                {
                    heavyTask();
                    strMsg[x] = $"Task {x + 1:00} finished\r\n";
                    Console.WriteLine(strMsg[x]);
                    this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
                });
            }
            Task tt = Task.WhenAll(tasks);
            tt.Wait();
        }

        private void heavyTask()
        {
            Thread.Sleep(1000);
        }
    }
}
