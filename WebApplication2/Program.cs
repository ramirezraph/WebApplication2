using WebApplication2.Data;
using WebApplication2.Repositories;
using WebApplication2.Repositories.MSSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EMSDBContext>();
builder.Services.AddScoped<IEmployeeDBRepository, EmployeeDBRepository>();
builder.Services.AddScoped<IDepartmentDBRepository, DepartmentDBRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
