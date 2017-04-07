﻿using JOB.DATA.Config;
using JOB.DATA.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JOB.DATA
{
    public class Contexto : DbContext
    {
        public Contexto()
           : base("AZURE")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<USUARIO> Usuario { get; set; }
        public DbSet<ENDERECO> Endereco { get; set; }
        public DbSet<CONTATO> Contato { get; set; }
        public DbSet<PERFIL_PROFISSIONAL> PerfilProfissional { get; set; }
        public DbSet<AVALIACAO> Avaliacao { get; set; }
        public DbSet<ESPECIALIDADE> Especialidade { get; set; }
        public DbSet<FORMACAO> Formacao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //plularização de objetos
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); //deleção em cascata de filho
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //deleção em cascata de relações n x n

            //o ideal é ter um esquema por projeto/sistema/área de atuação
            modelBuilder.HasDefaultSchema("jobup");

            //tipo padrão para string é varchar
            modelBuilder.Properties<string>()
                .Configure(p => p.IsUnicode(false));

            //tamanho padrão para string é de 254
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(254));

            //Configuring/Mapping Properties and Types with the Fluent API: https://msdn.microsoft.com/en-us/data/jj591617.aspx
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new EnderecoConfig());
            modelBuilder.Configurations.Add(new ContatoConfig());
            modelBuilder.Configurations.Add(new PerfilProfissionalConfig());
            modelBuilder.Configurations.Add(new AvaliacaoConfig());
            modelBuilder.Configurations.Add(new EspecialidadeConfig());
            modelBuilder.Configurations.Add(new FormacaoConfig());
        }
    }
}