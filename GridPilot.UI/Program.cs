using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using GridPilot.UI.Data;
using Microsoft.EntityFrameworkCore;
using GridPilot.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Ανάκτηση του Connection String από το appsettings.json
var connectionString = builder.Configuration.GetConnectionString("AccessConnection");

// Καταχώρηση του IDbContextFactory με τον Jet Provider για MS Access
builder.Services.AddDbContextFactory<GridPilotContext>(options =>
    options.UseJet(connectionString));


var app = builder.Build();

// Αυτόματος έλεγχος, δημιουργία βάσης/πινάκων και Seeding κατά την εκκίνηση
using (var scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<GridPilotContext>>();
    using var context = contextFactory.CreateDbContext();
    
    // Εφαρμόζει αυτόματα τα Migrations και δημιουργεί τους πίνακες στην Access
    context.Database.Migrate(); 
    
    // Γεμίζει τον Πίνακα V του ΔΕΔΔΗΕ
    DbSeeder.SeedParameters(context); 
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
