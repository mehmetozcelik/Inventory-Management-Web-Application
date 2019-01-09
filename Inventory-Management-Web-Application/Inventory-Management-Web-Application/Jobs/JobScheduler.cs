using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Inventory_Management_Web_Application.Jobs
{
    public class JobScheduler
    {
        public static void Start()
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<MailJob>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInHours(24)
                .RepeatForever())
                .Build();
                scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/Admin/Hata");
            }

        }
    }
}