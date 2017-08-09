using Hangfire;
using System.Threading.Tasks;

namespace TestCommon
{
    public interface IJobMethods
    {
        [Queue("email")]
        Task SendEmail(int jobId);

        [Queue("sms")]
        Task SendSms(int jobId);

        [Queue("phone")]
        Task SendPhone(int jobId);
    }
}
