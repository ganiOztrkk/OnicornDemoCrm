using Mvc.Attributes;
using Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthApiService>();
builder.Services.AddScoped<CustomerApiService>();
builder.Services.AddScoped<UserApiService>();


builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add(new AuthenticateAttribute());
    })
    .AddRazorRuntimeCompilation()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Pages/{0}.cshtml");
        options.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml");
    });

builder.Services.AddSession();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();