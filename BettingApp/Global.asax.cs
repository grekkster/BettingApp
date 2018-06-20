using BettingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BettingApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private BettingDBContext db = new BettingDBContext();
        private static Timer dbRandomOddsTimer;
        private const int MinOdd = 1;
        private const int MaxOdd = 10;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer<BettingDBContext>(new DropCreateDatabaseIfModelChanges<BettingDBContext>());
            Database.SetInitializer<BettingDBContext>(null);

            //var waitHandle = new AutoResetEvent(false);
            //ThreadPool.RegisterWaitForSingleObject(
            //    waitHandle,
            //    // Method to execute
            //    (state, timeout) =>
            //    {
            //        // TODO: implement the functionality you want to be executed
            //        // on every 5 seconds here
            //        // Important Remark: This method runs on a worker thread drawn 
            //        // from the thread pool which is also used to service requests
            //        // so make sure that this method returns as fast as possible or
            //        // you will be jeopardizing worker threads which could be catastrophic 
            //        // in a web application. Make sure you don't sleep here and if you were
            //        // to perform some I/O intensive operation make sure you use asynchronous
            //        // API and IO completion ports for increased scalability

            //        var matches = db.Matches;
            //        foreach (Match match in matches)
            //        {
            //            decimal step = 0.1m;
            //            match.Odds.Home = match.Odds.Home + step;

            //        }
            //        db.SaveChanges();
            //        //Console.WriteLine("Random akce.");
            //        //var isDbContext = db != null;

            //    },
            //    // optional state object to pass to the method
            //    null,
            //    // Execute the method after 5 seconds
            //    //TimeSpan.FromSeconds(5),
            //    TimeSpan.FromSeconds(new Random().Next(5,20)),
            //    //new Random(15000).Next() + 5000,
            //    // Set this to false to execute it repeatedly every 5 seconds
            //    false
            //);

            dbRandomOddsTimer = new Timer(OnTimer, null, new Random().Next(5000, 20000), Timeout.Infinite);
        }

        private void OnTimer(object state)
        {
            bool change;
            Random random = new Random();
            var matches = db.Matches;
            foreach (Match match in matches)
            {
                change = random.NextDouble() >= 0.5;
                if (change)
                {
                    change = random.NextDouble() >= 0.5;
                    if (change)
                    {
                        match.Odds.Home = Math.Round(random.NextDouble() * MaxOdd + MinOdd, 2);
                    }
                    change = random.NextDouble() >= 0.5;
                    if (change)
                    {
                        match.Odds.Draw = Math.Round(random.NextDouble() * MaxOdd + MinOdd, 2);
                    }
                    change = random.NextDouble() >= 0.5;
                    if (change)
                    {
                        match.Odds.Away = Math.Round(random.NextDouble() * MaxOdd + MinOdd, 2);
                    }
                }
            }
            db.SaveChanges();

            int randomTime = new Random().Next(5000, 20000);
            dbRandomOddsTimer.Change(randomTime, Timeout.Infinite);
        }
    }
}
