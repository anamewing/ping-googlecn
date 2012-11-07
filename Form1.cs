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
        Dictionary<string, pingresult> PRdic = new Dictionary<string, pingresult>();
        
        //DictionaryBindingList<string, pingresult> bs1;
        //pingresultBindingSource.DataSource=new BindingSource(PRdic,null);
        BindingSource bs1=new BindingSource();
        
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.DataSource = new BindingSource(PRdic, null);
            //var bs1 = PRdic.ToBindingList();
            //bs1 = new DictionaryBindingList<string, pingresult>(PRdic);
            dataGridView1.DataSource = bs1;
        
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            
            for (c = 45; c <= 47; c++)
            {
                for (d = 1; d <= 255; d++)
                {
                    now = rootstr + c + "." + d;
                    pingresult PRtemp = new pingresult(now);
                    PRdic.Add(now, PRtemp);
                    bs1.Add(PRtemp);
                }
            }
            timer1.Enabled = true;
        }

        private void pingcom(object sender, PingCompletedEventArgs e)
        {
            PingReply myrep = e.Reply;
            if (myrep.Status == IPStatus.Success)
            {
                PRdic[e.Reply.Address.ToString()].updateReply(myrep.RoundtripTime);
            }
            //((AutoResetEvent)e.UserState).Set();
        }

        void pingall()
        {
            for (c = 45; c <= 47; c++)
            {
                for (d = 1; d <= 255; d++)
                {
                    //ping他，返回只
                    now = rootstr + c + "." + d;
                    Ping myping = new Ping();
                    AutoResetEvent myreset = new AutoResetEvent(false);
                    myping.PingCompleted += new PingCompletedEventHandler(pingcom);
                    myping.SendAsync(rootstr + c + "." + d, myreset);
                    PRdic[now].addpingtimes();

                }
            }   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pingall();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }


    }

    

}
