using Microsoft.AspNetCore.DataProtection;
using MyApplication.Interfaces;
using MyApplication.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use((ctx, next) =>
{
    var dpp = ctx.RequestServices.GetRequiredService<IDataProtectionProvider>();
    var protector = dpp.CreateProtector("auth-cookie");
    var authCookie = ctx.Request
        .Headers.Cookie.FirstOrDefault(x => x.StartsWith("auth="));

    if (authCookie != null)
    {
        var protectedPayload = authCookie.Split("=").Last();
        var payload = protector.Unprotect(protectedPayload); // user:george
        var parts = payload.Split(":");
        var key = parts[0]; // user
        var value = parts[1]; // george

        var claims = new List<Claim>();
        claims.Add(new Claim(key, value));
        var identity = new ClaimsIdentity(claims);
        ctx.User = new ClaimsPrincipal(identity);
    }
    
    return next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
