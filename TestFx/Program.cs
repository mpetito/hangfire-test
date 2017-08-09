using Hangfire;
using System;
using System.Threading.Tasks;
using TestCommon;

namespace TestFx
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new BackgroundJobServerOptions
            {
                Queues = new[] { "default", "email" },
                Activator = new Activator(),
            };

            GlobalConfiguration.Configuration.UseColouredConsoleLogProvider();
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=(local);Database=Framework;Integrated Security=true");

            using (var server = new BackgroundJobServer(options))
            {
                BackgroundJob.Enqueue<IJobMethods>(job => job.SendEmail(1));
                BackgroundJob.Enqueue<IJobMethods>(job => job.SendSms(2));
                BackgroundJob.Enqueue<IJobMethods>(job => job.SendPhone(3));


                Console.WriteLine("TestFx Hangfire Server started. Press any key to exit...");
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
            Console.WriteLine("Send Email: {0}", jobId);

            return Task.CompletedTask;
        }

        public Task SendPhone(int jobId)
        {
            throw new NotSupportedException();
        }

        public Task SendSms(int jobId)
        {
            throw new NotSupportedException();
        }
    }
}
