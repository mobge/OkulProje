﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proje.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class okulEntities : DbContext
    {
        public okulEntities()
            : base("name=okulEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Acilan_Dersler> Acilan_Dersler { get; set; }
        public virtual DbSet<Bolum> Bolum { get; set; }
        public virtual DbSet<Bolum_Kazanim> Bolum_Kazanim { get; set; }
        public virtual DbSet<Ders_Kazanim> Ders_Kazanim { get; set; }
        public virtual DbSet<Dersler> Dersler { get; set; }
        public virtual DbSet<Donem_Yil> Donem_Yil { get; set; }
        public virtual DbSet<Fakulte> Fakulte { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Roller> Roller { get; set; }
        public virtual DbSet<Sınav_Turu> Sınav_Turu { get; set; }
        public virtual DbSet<Sinav_Grup> Sinav_Grup { get; set; }
        public virtual DbSet<Sinav_Sonuclari> Sinav_Sonuclari { get; set; }
        public virtual DbSet<Soru_Kazanim> Soru_Kazanim { get; set; }
    }
}
