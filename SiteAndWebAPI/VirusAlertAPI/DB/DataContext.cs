using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using VirusAlertAPI.Models;

namespace VirusAlertAPI.DB
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<Regions> regions { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<VirusData> virusData { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Limit> Limits { get; set; }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public VirusData VirusToday(int reg)
        {
            var today = DateTime.Now;
            var s = today.ToString("yyyy-MM-dd");
            var res = virusData.Where(x => x.vdRegId == reg && x.vdDate == s).FirstOrDefault();
            int iter = 10;
            while (res == null && iter>0)
            {
                iter--;
                today = today.AddDays(-1);
                res = virusData.Where(x => x.vdRegId == reg && x.vdDate == today.ToString("YYYY-MM-dd")).FirstOrDefault();
            }
            if (res == null)
                res = virusData.Last();
            return res;
        }
    }
}
