using Microsoft.EntityFrameworkCore;
using GridPilot.Data;
using GridPilot.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ITopographyEngine, TopographyEngine>();
builder.Services.AddScoped<IElectricalEngine, ElectricalEngine>();
builder.Services.AddScoped<IMechanicalEngine, MechanicalEngine>();
builder.Services.AddScoped<NetworkStudyService>();
builder.Services.AddScoped<NeutralizationService>();


// Ανάκτηση του Connection String από το appsettings.json
var connectionString = builder.Configuration.GetConnectionString("AccessConnection");

// Καταχώρηση του IDbContextFactory με τον Sqlite Provider
builder.Services.AddDbContextFactory<GridPilotContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));



var app = builder.Build();

// Αυτόματος έλεγχος, δημιουργία βάσης/πινάκων και Seeding κατά την εκκίνηση
using (var scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<GridPilotContext>>();
    using var context = contextFactory.CreateDbContext();
    
    // Εφαρμόζει αυτόματα τα Migrations και δημιουργεί τους πίνακες στην Access
     context.Database.EnsureCreated(); 
    
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
