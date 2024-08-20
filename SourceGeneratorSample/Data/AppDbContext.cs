using Microsoft.EntityFrameworkCore;
using SourceGeneratorSample.Entities;

namespace SourceGeneratorSample.Data;

public partial class AppDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MyDb42;Trusted_Connection=True;TrustServerCertificate=True;");
	}

	public DbSet<Account> Accounts { get; set; }
}