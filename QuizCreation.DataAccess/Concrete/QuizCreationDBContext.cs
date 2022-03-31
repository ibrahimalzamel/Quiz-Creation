using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.DataAccess.Concrete
{
    public class QuizCreationDBContext: DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Student> Students { get; set; }
        public QuizCreationDBContext(DbContextOptions<QuizCreationDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizCreationDb;Trusted_Connection=true;MultipleActiveResultSets=true;");
            //}
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users").HasKey(k => k.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(50);
                entity.Property(x => x.UserName).IsRequired().HasMaxLength(50);
                entity.Property(x => x.Password).IsRequired().HasMaxLength(50);

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students").HasKey(k => k.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(50);
                entity.Property(x => x.UserName).IsRequired().HasMaxLength(50);
                entity.Property(x => x.Password).IsRequired().HasMaxLength(50);
                entity.Property(x => x.Contact).HasMaxLength(50);
                entity.Property(x => x.CVFileName).HasMaxLength(250);
                entity.Property(x => x.PictureFileName).HasMaxLength(250);
                entity.HasOne(d=>d.Group).WithMany(p=>p.Students).HasForeignKey(d => d.GroupId);  

            });
            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.ToTable("QuestionAnswers").HasKey(k => k.Id);
                entity.Property(x => x.Question).IsRequired();
                entity.Property(x => x.Option1).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Option2).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Option3).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Option4).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Answer).IsRequired();
                entity.HasOne(d => d.Exam).WithMany(p => p.QuestionAnswers).HasForeignKey(d => d.ExamId).OnDelete(DeleteBehavior.ClientSetNull);

            });
            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups").HasKey(k => k.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Description).HasMaxLength(250);
                entity.HasOne(d => d.User).WithMany(p => p.Groups).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exams").HasKey(k => k.Id);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Description).HasMaxLength(250);
                entity.HasOne(d => d.Group).WithMany(p => p.Exams).HasForeignKey(d => d.GroupId).OnDelete(DeleteBehavior.ClientSetNull);

            });
            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.ToTable("ExamResults").HasKey(k => k.Id);
                entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults).HasForeignKey(d => d.ExamId);
                entity.HasOne(d => d.Student).WithMany(p => p.ExamResults).HasForeignKey(d => d.StudentId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.QuestionAnswer).WithMany(p => p.ExamResults).HasForeignKey(d => d.QuestionAnswerId).OnDelete(DeleteBehavior.ClientSetNull);

            });
        }



    }
}
