using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STS.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace STS.Infrastructure.DBContexts
{
    public class STSDBContext : DbContext
    {
        private readonly IHttpContextAccessor _context;
        public STSDBContext(DbContextOptions<STSDBContext> options, IHttpContextAccessor context) : base(options)
        {
            _context = context;
        }

        public DbSet<Governorate> Governorate { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CartItem> CartItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}


