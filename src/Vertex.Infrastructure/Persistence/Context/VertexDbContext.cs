using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vertex.Domain.Entities;

namespace Vertex.Infrastructure.Persistence.Context
{
    public class VertexDbContext : DbContext
    {
        public VertexDbContext(
            DbContextOptions<VertexDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<Computador> Computadores => Set<Computador>();

        public DbSet<Estacao> Estacoes => Set<Estacao>();

        public DbSet<Sessao> Sessoes => Set<Sessao>();

        public DbSet<ComputadorCredential> ComputadorCredentials 
            => Set<ComputadorCredential>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(VertexDbContext).Assembly);
        }

        public DbSet<TipoMaquina> TiposMaquina 
            => Set<TipoMaquina>();

        public DbSet<ConfiguracaoTarifacao> ConfiguracoesTarifacao
            => Set<ConfiguracaoTarifacao>();

        public DbSet<FaixaHorarioTarifacao> FaixasHorarioTarifacao
            => Set<FaixaHorarioTarifacao>();
    }
}
