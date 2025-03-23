using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AI_Vid_Automation.Models;

public partial class AividAutomationDbContext : DbContext
{
    public AividAutomationDbContext()
    {
    }

    public AividAutomationDbContext(DbContextOptions<AividAutomationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aiprompt> Aiprompts { get; set; }

    public virtual DbSet<VideoData> VideoData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AIVidAutomationDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aiprompt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AIPrompt__3214EC27E7DB2367");

            entity.ToTable("AIPrompt");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Prompt).IsUnicode(false);
        });

        modelBuilder.Entity<VideoData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VideoDat__3214EC278C63EBFE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idea).IsUnicode(false);
            entity.Property(e => e.Text).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
