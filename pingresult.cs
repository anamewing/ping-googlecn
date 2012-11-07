using System;
using System.Collections.Generic;
using System.Text;

namespace pinggoogle
{
    class pingresult
    {
        public string ip{get;set;}
        public double RTtime { get; set; }
        //DateTime time;
        public int pingtimes { get; set; }
        public int replytimes { get; set; }
        public double loserate { get; set; }

        double calLoserate()
        {
            try
            {
                loserate = 1 - replytimes / Convert.ToDouble(pingtimes);
            }

            catch (DivideByZeroException)
            {
                loserate = 1;
            }
                
            
            return loserate;
        }

        public pingresult(string IP)
        {
            ip = IP;
            RTtime = 3000;
            pingtimes = 0;
            replytimes = 0;
        }

        public void addpingtimes()
        {
            pingtimes += 1;
            calLoserate();
        }

        public void addreplytimes()
        {
            replytimes += 1;
            calLoserate();
        }

        public void updateReply(double time)
        {
            RTtime=(RTtime*replytimes+time)/(replytimes+1);
            addreplytimes();
        }

    }
}
