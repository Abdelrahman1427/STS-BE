using STS.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace STS.Infrastructure.DBContexts
{
    public class STSDBContext : DbContext
    {
        private readonly IHttpContextAccessor _context;
        public STSDBContext(DbContextOptions<STSDBContext>options, IHttpContextAccessor context) : base(options)
        {
            _context = context;
        }

        public DbSet<Logger> Logger { get; set; }
        public DbSet<Governorate> Governorate { get; set; }


    }
}