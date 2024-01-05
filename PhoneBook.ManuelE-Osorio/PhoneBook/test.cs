using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace PhoneBookProgram;
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Contact> Contacs {get; set;}
    public DbSet<Email> Emails {get; set;} 
    public DbSet<PhoneNumber> PhoneNumbers {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
        .UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)
        .LogTo(Console.WriteLine, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Deck>()
        //     .HasKey(deck => deck.DeckName);

        // modelBuilder.Entity<Deck>()
        //     .Property(deck => deck.DeckName)
        //     .IsRequired()
        //     .IsUnicode()
        //     .HasMaxLength(50);

        // modelBuilder.Entity<Deck>()
        //     .Property(deck => deck.DeckID)
        //     .IsRequired()
        //     .UseIdentityColumn();

        // modelBuilder.Entity<Deck>()
        //     .OwnsMany<FlashCard>( "FlashCards");

        // modelBuilder.Entity<Deck>()
        //     .OwnsMany<StudySession>( "StudySessions");
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}

public class Contact
{
    public int ContactId {get; set;}
    public string? ContactName {get; set;}
    public List<PhoneNumber>? PhoneNumbers {get; set;}
    public List<Email>? Emails {get; set;}
}

// public class PhoneNumber
// {
//     public int ContactId {get; set;}
//     public string? Region {get; set;}
//     public string? LocalNumber {get; set;}

//     public Contact? Contact {get; set;}
// }

public class Email
{
    public int 
}


