using Microsoft.AspNetCore.Mvc;
using MyTaskTracker.Interfaces;
using MyTaskTracker.Models;
using MyTaskTracker.Repository;
using MyTaskTracker.ViewModels;

namespace MyTaskTracker.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        [HttpGet]
        [Route("Organization")]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var organization = category switch
            {
                -1 => await _organizationRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _organizationRepository.GetCountAsync(),
            };

            var organizationViewModel = new IndexOrganizationViewModel()
            {
                Organizations = organization,
                Page = page,
                PageSize = pageSize,
                TotalOrganizations = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            };

            return View(organizationViewModel);
        }
        
        [HttpGet]
        [Route("organization/detail{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var organization = await _organizationRepository.GetByIdAsync(id);

            return organization == null ? NotFound() : View(organization);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var orgDetails = await _organizationRepository.GetByIdAsync(id);
            if (orgDetails == null) return View("Error");
            return View(orgDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var orgDetails = await _organizationRepository.GetByIdAsync(id);

            if (orgDetails == null)
            {
                return View("Error");
            }

            _organizationRepository.Delete(orgDetails);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var organization = await _organizationRepository.GetByIdAsync(id);
            if (organization == null) return View("Error");
            var organizationVM = new EditOrganizationViewModel
            {
                Name = organization.Name,
                Description = organization.Description,
            };
            return View(organizationVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditOrganizationViewModel organizationVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", organizationVM);
            }

            var organization = await _organizationRepository.GetByIdAsyncNoTracking(id);

            if (organization == null)
            {
                return View("Error");
            }

            var newOrganization = new Organization
            {
                Id = id,
                Name = organizationVM.Name,
                Description = organizationVM.Description,
            };

            _organizationRepository.Update(newOrganization);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddOrganization()
        {
            var response = new AddOrganizationViewModel();
            return View(response);
        } 
    
        [HttpPost]
        public async Task<IActionResult> AddOrganization(AddOrganizationViewModel addOrganizationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addOrganizationViewModel);
            }
            else
            {
                var org = new Models.Organization
                {
                    Name = addOrganizationViewModel.Name,
                    Description = addOrganizationViewModel.Description,
                };
                _organizationRepository.Add(org);
            }
        
            return RedirectToAction("Index");
        }
    }
}

