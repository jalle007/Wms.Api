using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wms.Infrastructure.Enums;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Wms.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Wms.Infrastructure
{
    public static class DBSeed
    {

        public static async Task SeedAsync(WmsDbContext context, IServiceScope serviceScope)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<WmsDbContext>();
            var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();


            if (!roleManager.Roles.Any())
            // Seed ROLES
            {
            var roleNames = EnumExtensions.Names(typeof(RoleType)).ToList();
                foreach (var roleName in roleNames)
                {
                    if (!roleManager.RoleExistsAsync(roleName).Result)
                    {
                        var role = new IdentityRole { Name = roleName };
                        var result = roleManager.CreateAsync(role).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception($"Failed to create role: {result.Errors.First().Description}");
                        }
                    }
                }
            }

            if (!context.Users.Any())   // Seed USERS
            {
            var users = new User[]
        {
            new User { UserName = "system_admin", FirstName = "system", LastName = "admin", Password="stringQ123123!2#",  Email ="admin@admin.com",  Role = RoleType.SystemAdmin },
            new User { UserName = "admin", FirstName = "Admin", LastName = "User", Password="stringQ123123!2#",  Email ="admin@admin.com",  Role = RoleType.ManagerRole },
            new User { UserName = "vedran", FirstName = "Vedran", LastName = "Veca", Password="stringQ123123!2#",  Email ="vedran@admin.com",  Role = RoleType.UserRole},
            //new User { UserName = "jasmin", FirstName = "Jasmin", LastName = "Ibrisimbegovic", Password="stringQ123123!2#",  Email ="adnan@admin.com",  Role = RoleType.AdminRole}
        };
            foreach (var user in users)
            {
                await userService.CreateOrUpdateUser(user);
            }
            }

            if (!dbContext.Locations.Any())     // Seed Locations if not any
            {
                var locations = new List<Location>
            {
                new Location
                {
                    LocationName = "Berlin Central",
                    ShortCode = "BER",
                    Address = "Alexanderplatz 1",
                    City = "Berlin",
                    Country = "Germany"
                },
                new Location
                {
                    LocationName = "Munich Central",
                    ShortCode = "MUC",
                    Address = "Marienplatz 1",
                    City = "Munich",
                    Country = "Germany"
                },
                new Location
                {
                    LocationName = "Hamburg Central",
                    ShortCode = "HAM",
                    Address = "Rathausmarkt 1",
                    City = "Hamburg",
                    Country = "Germany"
                }
            };

                dbContext.AddRange(locations);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Warehouses.Any()) // Seed Warehouses if not any
            {
                var warehouses = new List<Warehouse>
            {
                new Warehouse {LocationId = 1,WarehouseName = "Warehouse 1"},
                new Warehouse {LocationId = 2,WarehouseName = "Warehouse 2"},
                new Warehouse {LocationId = 3,WarehouseName = "Warehouse 3"},
            };

                dbContext.AddRange(warehouses);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Areas.Any()) // Seed Areas if not any
            {
                var areas = new List<Area>
            {
                new Area {WarehouseId = 1,AreaName= "Area 1"},
                new Area {WarehouseId = 2,AreaName= "Area 2"},
                new Area {WarehouseId = 3,AreaName= "Area 3"},
            };

                dbContext.AddRange(areas);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Shelves.Any()) // Seed Shelves if not any
            {
                var shelves = new List<Shelf>
            {
                new Shelf {AreaId = 1, ShelfName= "Shelf 1"},
                new Shelf {AreaId = 1, ShelfName= "Shelf 1"},
                new Shelf {AreaId = 1, ShelfName= "Shelf 1"}
            };

                dbContext.AddRange(shelves);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.StorageLocations.Any()) // Seed StorageLocations if not any
            {
                var storageLocations = new List<StorageLocation>
            {
                new StorageLocation {LocationId=1, WarehouseId=1, AreaId=1, ShelfId=1,Row=10, Column=10},
                new StorageLocation {LocationId=2, WarehouseId=2, AreaId=2, ShelfId=2,Row=20, Column=10},
                new StorageLocation {LocationId=2, WarehouseId=2, AreaId=2, ShelfId=2,Row=30, Column=10},
            };

                dbContext.AddRange(storageLocations);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Samples.Any()) // Seed Samples if not any
            {
                var samples = new List<Sample>
            {
                new Sample {Barcode="Barcode 1", ParentId=1},
                new Sample {Barcode="Barcode 2", ParentId=2},
                new Sample {Barcode="Barcode 3", ParentId=3},
            };

                dbContext.AddRange(samples);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Orders.Any()) // Seed Orders if not any
            {
                var orders = new List<Order>
            {
                new Order {SampleId=1, ItemType=ItemType.Box,Item="Box 1", SourceId=1, TargetId=1 },
                new Order {SampleId=2, ItemType=ItemType.Box,Item="Box 2", SourceId=1, TargetId=1 },
                new Order {SampleId=3, ItemType=ItemType.Sample,Item="Sample 1", SourceId=1, TargetId=1 },
            };

                dbContext.AddRange(orders);
                await dbContext.SaveChangesAsync();
            }


            if (!dbContext.OrderTraces.Any()) // Seed OrderTraces if not any
            {
                var users = await userService.GetAllUsers();
                int c = 0;
                var orderTraces = new List<OrderTrace>();

                foreach (var user in users)
                {
                    c++;
                    orderTraces.Add(new OrderTrace { OrderId = c, UserId=user.Id });
                }

                dbContext.AddRange(orderTraces);
                await dbContext.SaveChangesAsync();

            }





        }
    }
}
