using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly ILogger<VacancyController> _logger;

        public VacancyController(IVacancyService vacancyService, ILogger<VacancyController> logger)
        {
            _vacancyService = vacancyService;
            _logger = logger;
        }


        [HttpPost("Create")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Create(VacancyPostDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var created = await _vacancyService.Create(model);
                    if (created)
                    {
                        return Ok(new { Message = "Vacancy created successfully" });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Failed to create vacancy" });
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Update(VacancyPutDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifiedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var updated = await _vacancyService.Update(model);
                    if (updated)
                    {
                        return Ok(new { Message = "Vacancy updated successfully" });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Failed to update vacancy" });
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var vacancy = await _vacancyService.GetById(id);
                if (vacancy != null)
                {
                    return Ok(vacancy);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var vacancies = await _vacancyService.GetAll();
                if (vacancies != null)
                {
                    return Ok(vacancies);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _vacancyService.Delete(id);
                if (deleted)
                {
                    return Ok(new { Message = "Vacancy deleted successfully" });
                }
                else
                {
                    return NotFound(new { Message = "Vacancy not found or deletion failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }

        [HttpPut("UpdateStatus")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateStatus(VacancyStatusDto vacancyStatusDto)
        {
            try
            {
                var posted = await _vacancyService.UpdateStatus(vacancyStatusDto);
                if (posted)
                {
                    return Ok(new { Message = "Vacancy updated successfully" });
                }
                else
                {
                    return NotFound(new { Message = "Vacancy not found or updated failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }


        [HttpGet("VacancyApplicantsList/{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> VacancyApplicantsList(Guid id)
        {
            try
            {
                var applicants = await _vacancyService.VacancyApplicantsList(id);
                if (applicants != null)
                {
                    return Ok(applicants);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }


        [HttpPost("Search")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> Search(VacancySearchDto vacancySearchDto)
        {
            try
            {
                var vacancies = await _vacancyService.Search(vacancySearchDto);
                if (vacancies != null)
                {
                    return Ok(vacancies);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }

        [HttpPost("Apply/{id}")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> Apply(Guid id)
        {
            try
            {
                var currentUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _vacancyService.Apply(id, currentUser);
                if (result == "Request applied successfully")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);

                return BadRequest(new { Message = "Server error" });
            }
        }
    }
}

