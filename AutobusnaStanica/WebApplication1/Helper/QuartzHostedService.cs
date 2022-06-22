using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Helper
{
    public class QuartzHostedService : IHostedService
    {

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<MyJob> _myJobs;

        public IScheduler Scheduler { get; set; }
        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<MyJob> myJobs)
        {
            _jobFactory = jobFactory;
            _schedulerFactory = schedulerFactory;
            _myJobs = myJobs;
        }

        //starta hosted service
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            //ako bih pravila vise procesa
            foreach (var myJob in _myJobs)
            {
                var job = CreateJob(myJob);
                var trigger = CreateTrigger(myJob);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
        }

        
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(MyJob myJob)
        {
            var Type = myJob.Type;
            return JobBuilder.Create(Type).WithIdentity(Type.FullName).WithDescription(Type.Name).Build();
        }
        private static ITrigger CreateTrigger(MyJob myJob)
        {
            var Type = myJob.Type;
            //za lokalno testiranje svako minut saljem
            return TriggerBuilder.Create()
                .WithIdentity($" { myJob.Type.FullName}.trigger")
                // .WithCronSchedule(myJob.Expression)
                //posalji svaki radni dan u 8
                .WithDailyTimeIntervalSchedule(s =>
                        s.WithIntervalInHours(24)
                        //s.WithIntervalInMinutes(1)
                        .OnMondayThroughFriday()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 0))
                )

                .Build();
        }


    }
}
