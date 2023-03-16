using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VeriTakipMvc.Models;

namespace VeriTakipMvc.Data
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceData> DeviceDatas { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<UserOfLevel> UserOfLevels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseSqlServer(connectionString: @"Server=BARAN\SQLEXPRESS;Database=VeriTakip;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;TrustServerCertificate=true");
			//options.UseSqlServer("Server=213.32.46.87;Database=Helpdesk;User=helpdesk_user;Password=xcoGSSlzsGTcurhUu1;");
		}

    }
}
