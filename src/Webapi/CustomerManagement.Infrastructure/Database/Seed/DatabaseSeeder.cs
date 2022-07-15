using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Utility.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Database.Seed;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;

    public DatabaseSeeder(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task Initialize()
    {
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();
        
        await LoadRoles();
        await LoadUsers();

        LoadCustomers();
    }

    private async Task LoadRoles()
    {
        await _roleManager.CreateAsync(new(){Name = AppRoleConstants.Admin});
        await _roleManager.CreateAsync(new(){Name = AppRoleConstants.Editor});
    }

    private async Task LoadUsers()
    {
        await _userManager.CreateAsync(new(){UserName = "adminuser", Email = "ahmetvepayev1@gmail.com"}, "adminPass123*");
        await _userManager.CreateAsync(new(){UserName = "ahmetvepayev", Email = "ahmetwepaogly@gmail.com"}, "ahmetPass123*");

        var user = await _userManager.FindByNameAsync("adminuser");
        await _userManager.AddToRolesAsync(user, new string[]{AppRoleConstants.Admin, AppRoleConstants.Editor});

        user = await _userManager.FindByNameAsync("ahmetvepayev");
        await _userManager.AddToRolesAsync(user, new string[]{AppRoleConstants.Editor});
    }

    private void LoadCustomers()
    {
        _context.Customers.AddRange(
            new Customer{
                // Id = 1
                Firstname = "Ahmet",
                LastName = "Vepayev",
                Email = "ahmetwepaogly@gmail.com",
                City = "Ankara",
                Phone = "11544788004998754",
                Photo = SeedConstants.PhotoBase64Png100x100White.ToByteArrayFromBase64(),
                CommercialTransactions = new List<CommercialTransaction>{
                    new CommercialTransaction{
                        Description = "Some transaction #1",
                        Amount = 100.00m,
                        Date = DateTime.UtcNow
                    },
                    new CommercialTransaction{
                        Description = "Some transaction #2",
                        Amount = 50.61m,
                        Date = DateTime.UtcNow
                    }
                }
            },
            new Customer{
                // Id = 1
                Firstname = "Name2",
                LastName = "Last2",
                Email = "mail2@gmail.com",
                City = "City2",
                Phone = "1002254778",
                Photo = SeedConstants.PhotoBase64Png100x100White.ToByteArrayFromBase64(),
                CommercialTransactions = new List<CommercialTransaction>{
                    new CommercialTransaction{
                        Description = "Some transaction #3",
                        Amount = 784.69m,
                        Date = DateTime.UtcNow
                    },
                    new CommercialTransaction{
                        Description = "Some transaction #4",
                        Amount = 115.04m,
                        Date = DateTime.UtcNow
                    },
                    new CommercialTransaction{
                        Description = "Some transaction #5",
                        Amount = 58712.01m,
                        Date = DateTime.UtcNow
                    }
                }
            }
        );

        _context.SaveChanges();
    }
}