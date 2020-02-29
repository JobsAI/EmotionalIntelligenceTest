using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace EmotionalIntelligenceTest.Models
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Answer>().ToTable("Answers");
        //    modelBuilder.Entity<Question>().ToTable("Questions");
           
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(configuration["connectionStrings:DefaultConnection"]);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.ID)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.correct).HasColumnName("correct");

                entity.Property(e => e.QuestionID).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.ID)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
