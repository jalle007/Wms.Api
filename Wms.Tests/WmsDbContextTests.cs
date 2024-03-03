using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Infrastructure.Models;
using Wms.Infrastructure;

namespace Wms.Tests
{
    public class WmsDbContextTests
    {
        private readonly WmsDbContext _context;

        public WmsDbContextTests()
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<WmsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Use in-memory database
                .Options;

            // Create instance of DbContext
            _context = new WmsDbContext(options);

            // Add entities in database
            _context.Users.Add(new User { /* set properties here */ });
            // Add other entities...

            _context.SaveChanges();
        }

        // Now we can write tests here...
    }

}
