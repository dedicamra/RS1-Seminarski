using Microsoft.AspNetCore.Http;
using Podaci.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Helper
{
    public static class AutentifikacijaMVC
    {
        public static string currentUserId;

        public static void SetLogiraniKorisnik(this HttpContext httpContext, Korisnik k)
        {
            httpContext.Session.Set<Korisnik>("nekiKljucVarijabla", k);
        }
        public static Korisnik GetLogiraniKorisnik(this HttpContext httpContext)
        {
            var k = httpContext.Session.Get<Korisnik>("nekiKljucVarijabla");
            return k;
        }
        public static Korisnik GetLogiranogUsera(this HttpContext httpContext)
        {
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Korisnik user =  db.Menadzer.FirstOrDefault(m => m.Id == currentUserId);
            if (user == null)
                user =  db.Kupac.FirstOrDefault(m => m.Id == currentUserId);
            return user;
        }
        public static bool ProvjeriKorisnika(this HttpContext httpContext,string userID)
        {
            //true ako je menadzer
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();
            var user = db.Menadzer.Where(m => m.Id == userID).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }
    }
}
