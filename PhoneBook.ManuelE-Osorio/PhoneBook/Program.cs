using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


// var folder = Environment.path;
// var path = System.IO.Path.GetDirectoryName(System.IO.Path.)
// var DbPath = System.IO.Path.Join(path, "blogging.db");


using var db = new BloggingContext();

Console.WriteLine(db.Database.EnsureDeleted());
Console.WriteLine(db.Database.EnsureCreated());

Console.WriteLine(db.Database.GenerateCreateScript());




// // Note: This sample requires the database to be created before running.
// Console.WriteLine($"Database path: ");

// // Create
// Console.WriteLine("Inserting a new blog");
// db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
// db.SaveChanges();

// // Read
// Console.WriteLine("Querying for a blog");
// var blog = db.Blogs
//     .OrderBy(b => b.BlogId)
//     .First();

// // Update
// Console.WriteLine("Updating the blog and adding a post");
// blog.Url = "https://devblogs.microsoft.com/dotnet";
// blog.Posts.Add(
//     new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
// db.SaveChanges();

// Delete
// Console.WriteLine("Delete the blog");
// db.Remove(blog);
// db.SaveChanges();