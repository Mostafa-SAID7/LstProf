using LstProf.Models;
using Microsoft.EntityFrameworkCore;

namespace LstProf.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            // ----- Seed Categories -----
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Web Development" },
                    new Category { Name = "Mobile App" },
                    new Category { Name = "Design" },
                    new Category { Name = "Data Science" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // ----- Seed Users -----
          

            // ----- Seed Tags -----
            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Name = "ASP.NET Core" },
                    new Tag { Name = "Tailwind CSS" },
                    new Tag { Name = "C#" },
                    new Tag { Name = "Entity Framework" },
                    new Tag { Name = "SQL Server" },
                    new Tag { Name = "Design" }
                );
                context.SaveChanges();
            }

            // ----- Seed Projects -----
            if (!context.Projects.Any())
            {
                var webCategory = context.Categories.First(c => c.Name == "Web Development");
                var mobileCategory = context.Categories.First(c => c.Name == "Mobile App");

                var project1 = new Project
                {
                    Title = "Portfolio Website",
                    Description = "My personal portfolio website",
                    LongDescription = "Showcasing projects, blogs, and resume online.",
                    TechnologyStack = "ASP.NET Core, Tailwind CSS, FontAwesome",
                    GitHubUrl = "https://github.com/mostafa/portfolio",
                    LiveUrl = "https://portfolio.example.com",
                    ImageUrl = "~/images/Projects/p4.png",
                    ThumbnailUrl = "~/images/Projects/p4.png",
                    CategoryId = webCategory.Id,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow,
                    Slug = "portfolio-website"
                };

                var project2 = new Project
                {
                    Title = "E-Commerce Site",
                    Description = "Online shopping platform",
                    LongDescription = "Full-stack e-commerce application with payments and admin panel.",
                    TechnologyStack = "ASP.NET Core, EF Core, SQL Server, Tailwind CSS",
                    GitHubUrl = "https://github.com/mostafa/ecommerce",
                    LiveUrl = "https://shop.example.com",
                    ImageUrl = "~/images/Projects/p3.png",
                    ThumbnailUrl = "~/images/Projects/p3.png",
                    CategoryId = webCategory.Id,
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow,
                    Slug = "ecommerce-site"
                };

                var project3 = new Project
                {
                    Title = "Mobile Chat App",
                    Description = "Cross-platform mobile messaging app",
                    LongDescription = "Real-time messaging with push notifications and media sharing.",
                    TechnologyStack = "Xamarin, Firebase, C#",
                    GitHubUrl = "https://github.com/mostafa/chatapp",
                    LiveUrl = "",
                    ImageUrl = "~/images/Projects/p2.png",
                    ThumbnailUrl = "~/images/Projects/p2.png",
                    CategoryId = mobileCategory.Id,
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow,
                    Slug = "mobile-chat-app"
                };

                context.Projects.AddRange(project1, project2, project3);
                context.SaveChanges();
            }

            // ----- Seed BlogPosts -----
            if (!context.BlogPosts.Any())
            {
                

                context.BlogPosts.AddRange(
                    new BlogPost
                    {
                        Title = "Getting Started with ASP.NET Core",
                        Content = "Full tutorial on creating ASP.NET Core apps...",
                        Summary = "Learn ASP.NET Core basics and project setup.",
                        Slug = "getting-started-with-aspnet-core",
                        MetaTitle = "ASP.NET Core Guide",
                        FeaturedImageUrl = "~/images/Blogs/b5.png",
                        IsPublished = true,
                        PublishedAt = DateTime.UtcNow
                    },
                    new BlogPost
                    {
                        Title = "Tailwind CSS Tips",
                        Content = "Useful tips for modern UI designs using Tailwind CSS.",
                        Summary = "Improve your Tailwind CSS skills with best practices.",
                        Slug = "tailwind-css-tips",
                        MetaTitle = "Tailwind CSS Guide",
                        FeaturedImageUrl = "~/images/Blogs/b3.png",
                        IsPublished = true,
                        PublishedAt = DateTime.UtcNow
                    },
                    new BlogPost
                    {
                        Title = "Entity Framework Core Migrations",
                        Content = "Step-by-step guide on EF Core migrations and seeding.",
                        Summary = "Learn how to manage database with EF Core.",
                        Slug = "ef-core-migrations",
                        MetaTitle = "EF Core Migrations",
                        FeaturedImageUrl = "~/images/Blogs/b1.png",
                        IsPublished = true,
                        PublishedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
