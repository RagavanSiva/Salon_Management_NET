using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon_Management_NET.Data;

namespace Salon_Management_NET.Controllers
{
    [ApiController]
    [Route("api/branch")]
    public class BranchController:Controller
    {
        private readonly AppAPIDbContext dbContext;

        public BranchController(AppAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> getBranches()
        {
            return Ok(await dbContext.Branches.ToListAsync());
        }

    }
}
