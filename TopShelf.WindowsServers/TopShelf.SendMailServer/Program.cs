using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using TopShelf.Job;

namespace TopShelf.SendMailServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HostFactory.Run(x =>
                {
                    x.RunAsLocalSystem();

                    x.SetServiceName("TopShelf.SendMailJob");
                    x.SetDisplayName("TopShelf.SendMailJob");
                    x.SetDescription("using topshelf to host windwdos server,processing sendMail");

                    x.Service<SendMailJob>(s =>
                    {
                        s.ConstructUsing(() => new SendMailJob());  //设备windowsServer的Job
                        s.WhenStarted(biz => biz.Process());
                        s.WhenStopped(biz => biz.Quit());
                    });
                });
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e.ToString());
#endif

            }
        }
    }
}
