using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudPractice1.Models;

namespace CrudPractice1.Data
{
    public class CrudPractice1Context : DbContext
    {
        public CrudPractice1Context (DbContextOptions<CrudPractice1Context> options)
            : base(options)
        {
        }

        public DbSet<CrudPractice1.Models.Movie> Movie { get; set; } = default!;
        public DbSet<CrudPractice1.Models.Movie2> Movie2 { get; set; } = default!;
        public DbSet<Language> Languages { get; set; } = default!;
        public DbSet<MovieLanguage> MovieLanguages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定 MovieLanguage 為複合主鍵
            modelBuilder.Entity<MovieLanguage>()
                .HasKey(ml => new { ml.Movie2Id, ml.LanguageCode });

            modelBuilder.Entity<MovieLanguage>()
                .HasOne(ml => ml.Movie2)
                .WithMany(m => m.MovieLanguages)
                .HasForeignKey(ml => ml.Movie2Id);

            modelBuilder.Entity<MovieLanguage>()
                .HasOne(ml => ml.Language)
                .WithMany(l => l.MovieLanguages)
                .HasForeignKey(ml => ml.LanguageCode);
            modelBuilder.Entity<Language>().HasData(
                new Language { Code = "zh", Name = "中文" },
                new Language { Code = "en", Name = "英文" },
                new Language { Code = "jp", Name = "日文" }
            );

            modelBuilder.Entity<Book>()
                .Property(b => b.Format)
                .HasConversion<string>();

            //Book-Category 多對多透過 BookCategory（顯式中介表）
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }
        // Book 和 Category 相關資料表
        public DbSet<CrudPractice1.Models.Book> Book { get; set; } = default!;
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; } = default!;

    }
}
