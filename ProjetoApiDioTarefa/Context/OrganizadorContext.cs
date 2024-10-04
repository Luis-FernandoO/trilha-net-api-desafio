using Microsoft.EntityFrameworkCore;
using ProjetoApiDioTarefa.Models;

namespace ProjetoApiDioTarefa.Context;

public class OrganizadorContext : DbContext
{
	public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
	{

	}
	public DbSet<Tarefa> Tarefas { get; set; }
}