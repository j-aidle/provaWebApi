﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace insti.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class instiEntities1 : DbContext
    {
        public instiEntities1()
            : base("name=instiEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<alumne> alumne { get; set; }
        public virtual DbSet<assignatura> assignatura { get; set; }
        public virtual DbSet<dada> dada { get; set; }
        public virtual DbSet<profe> profe { get; set; }
    }
}
