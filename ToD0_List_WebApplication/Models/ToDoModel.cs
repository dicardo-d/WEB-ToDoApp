using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ToD0_List_WebApplication.Models
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoModel> ToDo => Set<ToDoModel>();

        public string DbPath { get; }

        public ToDoContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "toDo.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class ToDoModel
	{
		[Key]
		public int TodoModelId { get; set; }
		[Required]
		public bool IsCompleted { get; set; }
		[Required]
		public DateTime CompletionDate { get; set; }
		[Required]
		public String? Description { get; set; }
	}
}

