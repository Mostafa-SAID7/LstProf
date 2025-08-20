using LstProf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1️⃣ Configure Services
// ---------------------------

// Add controllers with views
builder.Services.AddControllersWithViews();

// Add DbContext with SQL Server (connection string from appsettings)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// ---------------------------
// 2️⃣ Seed Data
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}

// ---------------------------
// 3️⃣ Configure Middleware
// ---------------------------

// Serve static files (wwwroot)
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Optional: Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable authorization (if you add Identity later)
app.UseAuthorization();

// ---------------------------
// 4️⃣ Map Routes
// ---------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Run the app
app.Run();



