using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JwtAuthntication.Model;
using EcommerceApiTest.Model;

namespace EcommerceApiTest.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext (DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public DbSet<JwtAuthntication.Model.Catigorie> Catigorie { get; set; } = default!;

        public DbSet<JwtAuthntication.Model.SubCatigorie>? SubCatigorie { get; set; }

        public DbSet<JwtAuthntication.Model.Product>? Product { get; set; }

        public DbSet<JwtAuthntication.Model.Order>? Order { get; set; }

        public DbSet<JwtAuthntication.Model.Provider>? Provider { get; set; }

        public DbSet<JwtAuthntication.Model.Customer>? Customer { get; set; }

        public DbSet<JwtAuthntication.Model.Admin>? Admin { get; set; }

        public DbSet<JwtAuthntication.Model.User>? User { get; set; }

        public DbSet<EcommerceApiTest.Model.Picture>? Picture { get; set; }
    }
}
