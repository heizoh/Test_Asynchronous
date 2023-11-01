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

        private void button1_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();

            var nums = Enumerable.Range(0, 5);
            foreach (var  x in nums)
            {
                Task t = Task.Run(() =>
                {
                    heavyTask();
                    strMsg[x] = $"Task {x + 1:00} finished\r\n";
                    Console.WriteLine(strMsg[x]);
                    //this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
                });
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    var x = i;
            //    Task t = Task.Run(() =>
            //    {
            //        heavyTask();
            //        strMsg[x] = $"Task {x + 1:00} finished\r\n";
            //        this.Invoke((MethodInvoker)(() => textBox1.AppendText(strMsg[x])));
            //    });
            //}

            Task tt = Task.WhenAll(tasks);
            tt.Wait();
            for (int i = 0; i < 5; i++) 
            {
                textBox1.AppendText(strMsg[i]);
            }
            Console.WriteLine("All task fin");
            textBox1.AppendText("All Proccess Finished");

        }

        private void heavyTask()
        {
            Thread.Sleep(1000);
        }
    }
}
