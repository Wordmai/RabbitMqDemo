using RabbitMQ.Client;
using RabbitMQDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    class Program
    {

        private static System.Timers.Timer timer;

        static RabbitMQHelper helper;

        static void Main(string[] args)
        {
            helper = new RabbitMQHelper("Demoexchange", "Demoqueue", ExchangeType.Direct, "Demoqueue", "MES", "123456", "10.33.22.154", 5672, "DemoHost");
            helper.MQMsg += Helper_MQMsg;
            helper.MQError += Helper_MQError;

            timer = new System.Timers.Timer(2000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            helper.Producer();
        }

        private static void Helper_MQMsg(string msg)
        {
            Console.WriteLine("已发送： {0}", msg);
        }
        private static void Helper_MQError(string error)
        {
            Console.WriteLine("错误信息： {0}", error);
        }
        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int i = 1;
            while (i <= 7)
            {
                helper.SendMsg($"This is a RabbitMQ Message  Whitch Send in{DateTime.Now.ToString()}");
                Console.WriteLine("Send Message success!");
                i++;
            }
        }
    }
}
