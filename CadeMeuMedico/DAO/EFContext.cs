using CadeMeuMedico.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.DAO {
    public class EFContext : DbContext{
        public EFContext() : base("EFConnectionString") { }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Medico>().ToTable("Medico");
            modelBuilder.Entity<Especialidade>().ToTable("Especialidade");
            modelBuilder.Entity<Cidade>().ToTable("Cidade");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Medico>().HasKey(m => new { m.MedicoID, m.DataAdd });
        }
    }
}