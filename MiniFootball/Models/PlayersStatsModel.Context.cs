﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniFootball.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ArcadiaFootballEntities : DbContext
    {
        public ArcadiaFootballEntities()
            : base("name=ArcadiaFootballEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<GameStats> GameStats { get; set; }
        public virtual DbSet<Stats> Stats { get; set; }
    }
}
