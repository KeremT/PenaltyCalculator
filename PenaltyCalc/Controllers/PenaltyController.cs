using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PenaltyCalc.Models;

namespace WebApplication1.Controllers
{

    public class PenaltyController : Controller
    {
        private CountryDBContext db = new CountryDBContext();

        // GET: Penalty
        public ActionResult Index()
        {
            return View(db.Country.ToList());
        }
        public ActionResult Result(int d, int m, int y, int d2, int m2, int y2, int c)
        {
            DateTime dt1 = new DateTime(y, m, d);
            DateTime dt2 = new DateTime(y2, m2, d2);
            int allowedDays = 10;
            int dif = (dt2 - dt1).Days;
            int dow = findDayOfWeek(dt1.DayOfWeek.ToString());
            Country country = db.Country.Find(c);

            //business days = 1, non-business days = 0
            int[] countryDays = { country.Monday, country.Tuesday, country.Wednesday, country.Thursday, country.Friday, country.Saturday, country.Sunday };
            
            // business days per week 
            int numOfBDPerWeek = findNumberOfBDPerWeek(countryDays);

            int numOfBD = ((dif) / 7) * numOfBDPerWeek;

            //adding remaining days after full weeks
            numOfBD += findNumberOfRemainingBD(dif,dow,countryDays);
           
            numOfBD = numOfBD - allowedDays;

            float penalty = numOfBD * country.CPUSD * 5;

            ViewBag.Result = penalty + country.Currency;

            return View();
        }

        private int findDayOfWeek(string v)
        {
            int dow = 0;
            switch (v)
            {

                case "Monday": dow = 0; break;
                case "Tuesday": dow = 1; break;
                case "Wednesday": dow = 2; break;
                case "Thursday": dow = 3; break;
                case "Friday": dow = 4; break;
                case "Saturday": dow = 5; break;
                case "Sunday": dow = 6; break;
            }
            return dow;
        }
        private int findNumberOfBusinessDays()
        {

            return 0;
        }
        private int findNumberOfBDPerWeek(int[] countryDays )
        {
            int numOfBDPerWeek = 0;
            foreach (int i in countryDays)
            {
                numOfBDPerWeek += i;
            }
            return numOfBDPerWeek;
        }
        private int findNumberOfRemainingBD(int dif, int dow,int[] countryDays)
        {
            int remBD = 0;
            int rem = (dif) % 7;
            for (int i = dow; rem > 0; i = i % 7)
            {
                remBD += countryDays[i];

                rem--;
                i++;
                

            }

            return remBD;
        }
    } 
}