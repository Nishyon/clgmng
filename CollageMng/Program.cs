using CollageMng.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddSession();
var MyConStr = builder.Configuration.GetConnectionString("MyString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(MyConStr));

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseSession();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.Run();
