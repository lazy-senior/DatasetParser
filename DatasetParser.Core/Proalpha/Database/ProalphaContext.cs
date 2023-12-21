using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DatasetParser.Core.Proalpa.Database.Entity;
using Microsoft.Extensions.Configuration;
namespace DatasetParser.Core.Proalpa.Database;

public partial class ProalphaContext : DbContext
{

    public IConfiguration Configuration { get; }

    public ProalphaContext(DbContextOptions<ProalphaContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public virtual DbSet<BG_Kopf> BG_Kopf { get; set; }

    public virtual DbSet<DRC_Class> DRC_Classes { get; set; }

    public virtual DbSet<DRC_DSEntityAlloc> DRC_DSEntityAllocs { get; set; }

    public virtual DbSet<DRC_DataProvider> DRC_DataProviders { get; set; }

    public virtual DbSet<DRC_Dataset> DRC_Datasets { get; set; }

    public virtual DbSet<DRC_Entity> DRC_Entities { get; set; }

    public virtual DbSet<DRC_Instance> DRC_Instances { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Proalpha_Test"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BG_Kopf>(entity =>
        {
            entity
                .HasKey(e => e.BG_Kopf_Obj);
            entity
                .ToView("vt_BG_Kopf");

            entity.Property(e => e.BG_Kopf_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Dialog_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Formular)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.GeneratorTypen)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.LayoutTypen)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Print_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.SuccessorForms)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Systemoption)
                .HasMaxLength(24)
                .IsUnicode(false);

            entity
                .HasOne(e => e.Print_DRC_Instance)
                .WithMany()
                .HasPrincipalKey(e => e.DRC_Instance_Obj)
                .HasForeignKey(e => e.Print_DRC_Instance_Obj);

            entity
                .HasOne(e => e.Dialog_DRC_Instance)
                .WithMany()
                .HasPrincipalKey(e => e.DRC_Instance_Obj)
                .HasForeignKey(e => e.Dialog_DRC_Instance_Obj);

            entity
                .HasMany(e => e.DRC_DataProviders)
                .WithOne(e => e.Owning_BG_Kopf)
                .HasPrincipalKey(e => e.BG_Kopf_Obj)
                .HasForeignKey(e => e.Owning_Obj);

        });

        modelBuilder.Entity<DRC_Class>(entity =>
        {
            entity
                .HasKey(e => e.DRC_Class_Obj);
            entity
                .ToView("vt_DRC_Class");

            entity.Property(e => e.DRC_Class_ID)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Class_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Parent_DRC_Class_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Rend_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);

        });

        modelBuilder.Entity<DRC_DSEntityAlloc>(entity =>
        {
            entity
                .HasKey(e => e.DRC_DSEntityAlloc_Obj);

            entity
                .ToView("vt_DRC_DSEntityAlloc");

            entity.Property(e => e.DRC_DSEntityAlloc_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Dataset_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Entity_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity 
                .HasOne(e => e.DRC_Entity)
                .WithMany(e => e.DRC_DSEntityAllocations)
                .HasPrincipalKey(e => e.DRC_Entity_Obj)
                .HasForeignKey(e => e.DRC_DSEntityAlloc_Obj);

            entity 
                .HasOne(e => e.DRC_Dataset)
                .WithMany(e => e.DRC_DSEntityAllocations)
                .HasPrincipalKey(e => e.DRC_Dataset_Obj)
                .HasForeignKey(e => e.DRC_DSEntityAlloc_Obj);
        });

        modelBuilder.Entity<DRC_DataProvider>(entity =>
        {
            entity
                .HasKey(e => e.DRC_DataProvider_Obj);
            entity
                .ToView("vt_DRC_DataProvider");

            entity.Property(e => e.DAO_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_DataProvider_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Dataset_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Owning_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Ref_DRC_DataProvider_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity 
                .HasOne(e => e.DAO_DRC_Instance)
                .WithMany()
                .HasPrincipalKey(e => e.DRC_Instance_Obj)
                .HasForeignKey(e => e.DAO_DRC_Instance_Obj);

            entity 
                .HasOne(e => e.DRC_Dataset)
                .WithMany(e => e.DRC_DataProviders)
                .HasPrincipalKey(e => e.DRC_Dataset_Obj)
                .HasForeignKey(e => e.DRC_Dataset_Obj);

            entity 
                .HasOne(e => e.Owning_BG_Kopf)
                .WithMany(e => e.DRC_DataProviders)
                .HasPrincipalKey(e => e.BG_Kopf_Obj)
                .HasForeignKey(e => e.Owning_Obj);

        });

        modelBuilder.Entity<DRC_Dataset>(entity =>
        {
            entity
                .HasKey(e => e.DRC_Dataset_Obj);
            entity
                .ToView("vt_DRC_Dataset");

            entity.Property(e => e.BEO_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DAO_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Dataset_ID)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Dataset_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DatasetDefinitionFile)
                .HasMaxLength(24)
                .IsUnicode(false);
            entity.Property(e => e.DatasetVersion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ExportProperties)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Proxy_DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity 
                .HasOne(e => e.BEO_DRC_Instance)
                .WithMany()
                .HasPrincipalKey(e => e.DRC_Instance_Obj)
                .HasForeignKey(e => e.BEO_DRC_Instance_Obj);

            entity 
                .HasOne(e => e.DAO_DRC_Instance)
                .WithMany()
                .HasPrincipalKey(e => e.DRC_Instance_Obj)
                .HasForeignKey(e => e.DAO_DRC_Instance_Obj);

            entity 
                .HasOne(e => e.Proxy_DRC_Instance)
                .WithMany()
                .HasPrincipalKey(e => e.DRC_Instance_Obj)
                .HasForeignKey(e => e.Proxy_DRC_Instance_Obj);

            entity 
                .HasMany(e => e.DRC_DataProviders)
                .WithOne(e => e.DRC_Dataset)
                .HasPrincipalKey(e => e.DRC_Dataset_Obj)
                .HasForeignKey(e => e.DRC_Dataset_Obj);


            entity
                .HasMany(e => e.DRC_DSEntityAllocations)
                .WithOne(e => e.DRC_Dataset)
                .HasPrincipalKey(e => e.DRC_Dataset_Obj)
                .HasForeignKey(e => e.DRC_Dataset_Obj);

        });

        modelBuilder.Entity<DRC_Entity>(entity =>
        {
            entity
                .HasKey(e => e.DRC_Entity_Obj);
            entity 
                .ToView("vt_DRC_Entity");

            entity.Property(e => e.DRC_Entity_ID)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Entity_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Table_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.EntityDefinitionFile)
                .HasMaxLength(24)
                .IsUnicode(false);

            entity
                .HasMany(e => e.DRC_DSEntityAllocations)
                .WithOne(e => e.DRC_Entity)
                .HasPrincipalKey(e => e.DRC_Entity_Obj)
                .HasForeignKey(e => e.DRC_Entity_Obj);

        });

        modelBuilder.Entity<DRC_Instance>(entity =>
        {
            entity
                .HasKey(e => e.DRC_Instance_Obj);
            
            entity
                .ToView("vt_DRC_Instance");

            entity.Property(e => e.DRC_Class_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Instance_ID)
                .HasMaxLength(110)
                .IsUnicode(false);
            entity.Property(e => e.DRC_Instance_Obj)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Directory)
                .HasMaxLength(120)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
