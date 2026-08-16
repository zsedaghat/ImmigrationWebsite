using ImmigrationWebsite.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ImmigrationWebsite.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<ConsultationRequest> ConsultationRequests { get; set; }

    public DbSet<ContactMessage> ContactMessages { get; set; }
}