using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon_Management_NET.Data;
using Salon_Management_NET.Model;
using Salon_Management_NET.Model.RequestDTO;
using System;

namespace Salon_Management_NET.Controllers
{
    [Route("api/ServiceCategory")]
    [ApiController]
    public class ServiceCategoryController : Controller
    {

        private readonly AppAPIDbContext _context;

        public ServiceCategoryController(AppAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategory>>> GetServiceCategories()
        {
            return await _context.ServiceCategories.ToListAsync();
        }

        // Get a specific service category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCategory>> GetServiceCategory(Guid id)
        {
            var serviceCategory = await _context.ServiceCategories.FindAsync(id);

            if (serviceCategory == null)
            {
                return NotFound();
            }

            return serviceCategory;
        }


        // POST: api/ServiceCategory
        [HttpPost]
        public async Task<IActionResult> CreateServiceCategory([FromBody] ServiceCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check for duplicate name
            if (_context.ServiceCategories.Any(sc => sc.Name == request.Name))
            {
                ModelState.AddModelError("Name", "Service category with this name already exists.");
                return BadRequest(ModelState);
            }

            var serviceCategory = new ServiceCategory
            {
                Name = request.Name,
                Active = request.Status
            };

            _context.ServiceCategories.Add(serviceCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetServiceCategory", new { id = serviceCategory.Id }, serviceCategory);
        }

        // PUT: api/ServiceCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceCategory(Guid id, [FromBody] ServiceCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceCategory = await _context.ServiceCategories.FindAsync(id);

            if (serviceCategory == null)
            {
                ModelState.AddModelError("Id", "Service category not found.");
                return BadRequest(ModelState);
            }

            // Check for duplicate name excluding the current service category
            if (_context.ServiceCategories.Any(sc => sc.Name == request.Name && sc.Id != id))
            {
                ModelState.AddModelError("Name", "Service category with this name already exists.");
                return BadRequest(ModelState);
            }

            serviceCategory.Name = request.Name;
            serviceCategory.Active = request.Status;

            _context.Entry(serviceCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceCategoryExists(id))
                {
                    ModelState.AddModelError("Id", "Service category not found.");
                    return BadRequest(ModelState);
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Update successful", serviceCategory });
        }


        // Delete a service category by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceCategory(Guid id)
        {
            var serviceCategory = await _context.ServiceCategories.FindAsync(id);

            if (serviceCategory == null)
            {
                return NotFound();
            }

            _context.ServiceCategories.Remove(serviceCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceCategoryExists(Guid id)
        {
            return _context.ServiceCategories.Any(e => e.Id == id);
        }

    }
}
