using Gedo.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<BudgetContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<BillContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ClientContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ConceptContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<UserContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      policy =>
      {
          policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
