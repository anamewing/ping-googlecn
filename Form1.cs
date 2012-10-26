using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;

namespace pinggoogle
{
    public partial class Form1 : Form
    {
        int c=148, d=1;
        string rootstr = "203.208.";
        string now = "";
        
        
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            for (c = 45; c <= 47; c++)
            {
                for (d = 1; d <= 255; d++)
                {
                    //ping他，返回只
                    now = rootstr + c + "." + d;
                    // textBox2.Text = now;
                    Ping myping = new Ping();
                    AutoResetEvent myreset = new AutoResetEvent(false);
                    myping.PingCompleted += new PingCompletedEventHandler(pingcom);
                    myping.SendAsync(rootstr + c + "." + d, myreset);

                    //PingReply myrep = myping.Send(now);
                    //if (myrep.Status == IPStatus.Success)
                    //{
                    //    textBox1.Text += now + "  time:" + myrep.RoundtripTime + "\r\n";
                    //}
                    
                }
            }   
        }
        private void pingcom(object sender, PingCompletedEventArgs e)
        {
            PingReply myrep = e.Reply;
            if (myrep.Status == IPStatus.Success)
            {
                textBox1.Text += e.Reply.Address + "  time:" + myrep.RoundtripTime + "ms\r\n";
            }
            ((AutoResetEvent)e.UserState).Set();
        }

    }
}
