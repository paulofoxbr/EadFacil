using EadFacil.APIv3.Data;
using Microsoft.EntityFrameworkCore;

namespace EadFacil.APIv3.Configurations;

public static class DatabaseSelectorExtension
{
    public static void AddDatabaseSelector(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<ApplicationDbContext>(opitions =>
                opitions.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
        else
        {
            builder.Services.AddDbContext<ApplicationDbContext>(opitions =>
                opitions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
    }
    
}