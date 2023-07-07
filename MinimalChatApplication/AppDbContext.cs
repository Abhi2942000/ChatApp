using Microsoft.EntityFrameworkCore;
using MinimalChatApplication.Models;
using System.Collections.Generic;

namespace MinimalChatApplication
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<UserRegistration> UserRegistrations { get; set; } 
        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<SendMessageRequest> SendMessageRequests { get; set; }

    }
}
