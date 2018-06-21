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
    public class MvcApplication : HttpApplication
    {
        private BettingDBContext db = new BettingDBContext();
        private static Timer dbRandomOddsTimer;
        private static Random random = new Random();
        private const int MinOdd = 1;
        private const int MaxOdd = 10;
        private const int MinDelayTime = 5000;
        private const int MaxDelayTIme = 20000;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // initialization
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BettingDBContext>());
            var matches = db.Matches;
            // if no matches in database, generate them
            if (matches != null && matches.Count() == 0)
                PopulateMatches(matches);
            // start randomly generating odds
            dbRandomOddsTimer = new Timer(ChangeOdds, null, random.Next(MinDelayTime, MaxDelayTIme), Timeout.Infinite);
        }

        /// <summary>
        /// Populate DB with randomly generated matches 
        /// </summary>
        /// <param name="matches">DB matches</param>
        private void PopulateMatches(DbSet<Match> matches)
        {
            var generatedMatches = new List<Match>()
            {
                new Match () {ID = 1, TeamHome = "Uruguay", TeamAway="Rusko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Egypt", TeamAway="Saúdská Arábie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Írán", TeamAway="Portugalsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Španělsko", TeamAway="Maroko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Dánsko", TeamAway="Austrálie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Francie", TeamAway="Peru", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Dánsko", TeamAway="Francie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Austrálie", TeamAway="Peru", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Argentina", TeamAway="Chorvatsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Nigérie", TeamAway="Island", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Nigérie", TeamAway="Argentina", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Island", TeamAway="Chorvatsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Brazílie", TeamAway="Kostarika", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Srbsko", TeamAway="Švýcarsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Srbsko", TeamAway="Brazílie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Švýcarsko", TeamAway="Kostarika", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Jižní Korea", TeamAway="Mexiko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Německo", TeamAway="Švédsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Mexiko", TeamAway="Švédsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Jižní Korea", TeamAway="Německo", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Belgie", TeamAway="Tunisko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Anglie", TeamAway="Panama", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Panama", TeamAway="Tunisko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Anglie", TeamAway="Belgie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Japonsko", TeamAway="Senegal", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Polsko", TeamAway="Kolumbie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Japonsko", TeamAway="Polsko", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } },
                new Match () {ID = 1, TeamHome = "Senegal", TeamAway="Kolumbie", Odds = new Odds() {Home=1, Draw=2, Away=3}, Bets = new Bets() { Home = false, Draw = false, Away = false } }
            };
            matches.AddRange(generatedMatches);
            db.SaveChanges();
        }

        /// <summary>
        /// Timer callback randomly changing matches odds
        /// </summary>
        /// <param name="state">state object</param>
        private void ChangeOdds(object state)
        {
            var matches = db.Matches;
            if (matches == null)
                return;

            foreach (Match match in matches)
            {
                // change match?
                if (GetRandomBool())
                {
                    // change Home odds ?
                    match.Odds.Home = GetRandomBool() ? GetRandomOdd() : match.Odds.Home;

                    // change Draw odds ?
                    match.Odds.Draw = GetRandomBool() ? GetRandomOdd() : match.Odds.Draw;

                    // change Away odds ?
                    match.Odds.Away = GetRandomBool() ? GetRandomOdd() : match.Odds.Away;
                }
            }
            db.SaveChanges();

            int randomTime = random.Next(MinDelayTime, MaxDelayTIme);
            dbRandomOddsTimer.Change(randomTime, Timeout.Infinite);
        }

        /// <summary>
        /// Get random boolean value
        /// </summary>
        /// <returns></returns>
        private static bool GetRandomBool()
        {
            return random.NextDouble() >= 0.5;
        }

        /// <summary>
        /// Get random double odd value
        /// </summary>
        /// <returns></returns>
        private static double GetRandomOdd()
        {
            return Math.Round(random.NextDouble() * MaxOdd + MinOdd, 2);
        }
    }
}
