using Hangfire;
using System;
using System.Threading.Tasks;
using TestCommon;

namespace TestCore
{
    public class Program
    {
        public static void Main()
        {
            var options = new BackgroundJobServerOptions
            {
                Queues = new[] { "sms", "phone" },
                Activator = new Activator()
            };

            GlobalConfiguration.Configuration.UseColouredConsoleLogProvider();
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=(local);Database=Framework;Integrated Security=true");

            using (var server = new BackgroundJobServer(options))
            {
                Console.WriteLine("TestCore Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

    class Activator : JobActivator
    {
        public override object ActivateJob(Type jobType)
        {
            return new JobMethods();
        }
    }

    class JobMethods : IJobMethods
    {
        public Task SendEmail(int jobId)
        {
            throw new NotSupportedException();
        }

        public Task SendPhone(int jobId)
        {
            Console.WriteLine("Send Phone: {0}", jobId);

            return Task.CompletedTask;
        }

        public Task SendSms(int jobId)
        {
            Console.WriteLine("Send Sms: {0}", jobId);

            return Task.CompletedTask;
        }
    }
}
