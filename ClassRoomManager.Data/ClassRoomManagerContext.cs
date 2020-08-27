using ClassRoomManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class ClassRoomManagerContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAssignment> ActivityAssignments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<StudentClassDay> StudentClassDays { get; set; }
        public DbSet<ClassDay> ClassDays { get; set; }
        public DbSet<StudentFinalGrade> StudentFinalGrades { get; set; }

        public ClassRoomManagerContext(DbContextOptions<ClassRoomManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<ActivityAssignment>().ToTable("ActivityAssignment");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Period>().ToTable("Period");
            modelBuilder.Entity<StudentClassDay>().ToTable("StudentClassDay");
            modelBuilder.Entity<ClassDay>().ToTable("ClassDay");
            modelBuilder.Entity<StudentFinalGrade>().ToTable("StudentFinalGrade");
        }
    }
}
