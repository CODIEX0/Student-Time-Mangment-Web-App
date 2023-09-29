using Microsoft.EntityFrameworkCore;
using STMWeb.Areas.Identity.Data;
using STMWeb.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("STMWebContextConnection") ?? throw new InvalidOperationException("Connection string 'STMWebContextConnection' not found.");

//adding the STMWebContext DB Context to the app and building 
builder.Services.AddDbContext<STMWebContext>(options =>
    options.UseSqlServer(connectionString));

//adding the STMWebUser application user to the STMWebContext DB Context and building 
builder.Services.AddDefaultIdentity<STMWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<STMWebContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
