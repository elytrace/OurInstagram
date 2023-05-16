using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OurInstagram.Models;

public class OurDbContext : DbContext
{
    public OurDbContext(DbContextOptions<OurDbContext> options) : base(options)
    {
        
    }
}