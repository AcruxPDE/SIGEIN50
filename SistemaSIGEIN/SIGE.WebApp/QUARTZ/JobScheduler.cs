using Quartz;
using Quartz.Impl;
using System;


namespace SIGE.WebApp.QUARTZ
{
    public class JobScheduler
    {
        public static void IniciaEnvioCorreos()
        {
            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            //scheduler.Start();

            //IJobDetail job = JobBuilder.Create<Notifier>().Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithDailyTimeIntervalSchedule
            //      (s =>
            //         s.WithIntervalInSeconds(1)
            //        .OnEveryDay()
            //        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
            //      )
            //    .Build();

            //scheduler.ScheduleJob(job, trigger);
        }

        public static void IniciaVerificaCorreos()
        {
            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            //scheduler.Start();

            //IJobDetail job = JobBuilder.Create<Notifier>().Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithDailyTimeIntervalSchedule
            //      (s =>
            //         s.WithIntervalInSeconds(1)
            //        .OnEveryDay()
            //        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
            //      )
            //    .Build();

            //scheduler.ScheduleJob(job, trigger);
        }

    }
}