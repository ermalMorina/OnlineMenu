using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Persistence;
using OnlineMenu.Repositories;
using OnlineMenu.Validations;
using FluentValidation;
using OnlineMenu.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var _key = builder.Configuration["Jwt:Key"];
var _issuer = builder.Configuration["Jwt:Issuer"];
var _audience = builder.Configuration["Jwt:Audience"];
var _expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddAuthentication(x =>
{
    
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.Authority = "Authority URL";
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/myhub")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };

    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = _audience,
        ValidIssuer = _issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expirtyMinutes))
    };


});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WaiterRole", policy => policy.RequireRole("waiter"));
    options.AddPolicy("AdminRole", policy => policy.RequireRole("admin"));
    options.AddPolicy("SuperAdminRole", policy => policy.RequireRole("superadmin"));
});

builder.Services.AddMultiTenancy()
    .WithResolutionStrategy<HostResolutionStrategy>()
    .WithStore<DbContextTenantStore>();

builder.Services.AddDbContext<TenantAdminDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TenantAdmin")));

builder.Services.AddDbContext<OMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OMContext")));
//builder.Services.AddHangfireServer();
//builder.Services.AddHangfire(config =>
// config.UseStorage(new SqlServerStorage("Database=MultiTenantSingleDb;Trusted_Connection=True;MultipleActiveResultSets=true")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<OMContext>()
            .AddDefaultTokenProviders();
builder.Services.AddAuthorization();
builder.Services.AddSignalR();
//builder.Services.AddTransient<BackGroundTask>();
//var orderService = builder.Services.BuildServiceProvider().GetService<BackGroundTask>();
builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ITenantRepository, TenantRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ITableRepository, TableRepository>();
builder.Services.AddSingleton<ITokenGenerator>(new TokenGenerator(_key, _issuer, _audience, _expirtyMinutes));

builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
//builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TenantValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductOrderValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://*.localhost.com:3000/*").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

var app = builder.Build();

//app.UseHangfireServer();
//RecurringJob.AddOrUpdate(() => orderService.CheckUnacceptedOrders(), Cron.Minutely);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRouting();
}
app.UseCors();
app.UseMultiTenancy();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapHub<GroupHub>("/myhub").RequireCors(builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("https://*.localhost.com:3000/*").AllowCredentials();
    }); 
    endpoints.MapControllers();
});

app.Run();
