using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Service.Mail;
using Service.MQ;

namespace TopShelf.Job
{
    public class SendMailJob
    {
        public void Process()
        {
            //拉取fanout_queue_Mail队列的消息，并绑定到fanout_Mail交换机，routingkey为mail
            MQHelperFactory.Default().FanoutConsume(item =>
            {
                var mail = JsonConvert.DeserializeObject<MyMail>(item.Item2);
                MailHelper.SendMail(mail);
            }, "fanout_Mail", "fanout_queue_Mail", "mail", maxPriority: 100);
        }

        public void Quit()
        {
            //throw new NotImplementedException();
        }
    }
}
