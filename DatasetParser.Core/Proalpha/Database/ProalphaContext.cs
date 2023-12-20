using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DatasetParser.Core.Proalpa.Database.Entity;
using Microsoft.Extensions.Configuration;
namespace DatasetParser.Core.Proalpa.Database;

public partial class ProalphaContext : DbContext
{

    private readonly IConfiguration Configuration;

    public ProalphaContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public ProalphaContext(DbContextOptions<ProalphaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VT_BG_Kopf> VT_BG_Kopf { get; set; }

    public virtual DbSet<VT_DRC_Class> VT_DRC_Classes { get; set; }

    public virtual DbSet<VT_DRC_DSEntityAlloc> VT_DRC_DSEntityAllocs { get; set; }

    public virtual DbSet<VT_DRC_DataProvider> VT_DRC_DataProviders { get; set; }

    public virtual DbSet<VT_DRC_Dataset> VT_DRC_Datasets { get; set; }

    public virtual DbSet<VT_DRC_Entity> VT_DRC_Entities { get; set; }

    public virtual DbSet<VT_DRC_Instance> VT_DRC_Instances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Proalpha_Test"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VT_BG_Kopf>(entity =>
        {
            entity
                .HasNoKey()
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
        });

        modelBuilder.Entity<VT_DRC_Class>(entity =>
        {
            entity
                .HasNoKey()
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

        modelBuilder.Entity<VT_DRC_DSEntityAlloc>(entity =>
        {
            entity
                .HasNoKey()
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
        });

        modelBuilder.Entity<VT_DRC_DataProvider>(entity =>
        {
            entity
                .HasNoKey()
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
        });

        modelBuilder.Entity<VT_DRC_Dataset>(entity =>
        {
            entity
                .HasNoKey()
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
        });

        modelBuilder.Entity<VT_DRC_Entity>(entity =>
        {
            entity
                .HasNoKey()
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
        });

        modelBuilder.Entity<VT_DRC_Instance>(entity =>
        {
            entity
                .HasNoKey()
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
