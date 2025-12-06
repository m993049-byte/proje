using lab4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab5.Models;

namespace lab4.Data
{
    public class lab4Context : DbContext
    {
        public lab4Context (DbContextOptions<lab4Context> options)
            : base(options)
        {
        }

        public DbSet<lab4.Models.book> book { get; set; } = default!;
        public DbSet<lab4.mycata>  categories { get; set; }
        public DbSet<lab4.Orderdetail> Orderdetail { get; set; }
        public DbSet<lab5.Models.usersaccounts> usersaccounts { get; set; } = default!;
        public DbSet<lab4.Models.orders> orders { get; set; } = default!;
        public DbSet<lab4.Models.bookorder> bookorder { get; set; } = default!;
        public DbSet<lab4.Models.orderline> orderline { get; set; } = default!;


    }
}
