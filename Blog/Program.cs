using Blog_CoreLayer.Services;
using Blog_CoreLayer.Services.Category;
using Blog_CoreLayer.Services.Comments;
using Blog_CoreLayer.Services.FileManager;
using Blog_CoreLayer.Services.Posts;
using Blog_CoreLayer.Services.User;
using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<IPostService , PostService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddTransient<IMainPageService, MainPageService>();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddAuthorization(option =>
{
        option.AddPolicy("AdminPolicy", builder =>
        {
            builder.RequireRole("Admin");
        });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    
}).AddCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.AccessDeniedPath = "/";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ErrorHandler/500");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "Default",
        pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");
});
        
  

app.Run();
