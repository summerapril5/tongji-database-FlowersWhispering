using FlowersWhisperingAPI.Data;
using Oracle.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using FlowersWhisperingAPI.User;
using FlowersWhisperingAPI.Administrator;
using FlowersWhisperingAPI.Community;

var builder = WebApplication.CreateBuilder(args);

String connectionString = builder.Configuration.GetConnectionString("OracleDbConnection");

// 添加数据库上下文
builder.Services.AddDbContext<FlowersWhisperingContext>(options =>
    options.UseOracle(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加控制器支持
builder.Services.AddControllers();

// 添加CORS支持

// 添加 CORS 服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080", "http://www.p1nkhub.com") // 替换为你的前端地址
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();  // 如果你使用的是Cookie或认证
        });
});


// 添加JWT认证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey != null ? Encoding.UTF8.GetBytes(jwtKey) : throw new ArgumentNullException("Jwt:Key")),
    };
});

if (connectionString != null)
{
    builder.Services.AddUserService(connectionString);
    builder.Services.AddAdminService(connectionString);
    builder.Services.AddCommunityService(connectionString);
}

var app = builder.Build();

// 启用静态文件服务，确保 wwwroot 中的文件可以被访问
app.UseStaticFiles();
app.UseDefaultFiles();  // 使 index.html 成为默认页面
app.UseStaticFiles();


var scope = app.Services.CreateScope();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// 使用CORS
app.UseCors("AllowSpecificOrigin");

// 使用身份验证和授权
app.UseAuthentication();
app.UseAuthorization();

// 映射控制器
app.MapControllers();

app.Run();