//using AutoMapper;
//using Contracts;
//using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        //private readonly IRepositoryManager _repository;
        //private readonly ILoggerManager _logger;
        //private readonly IMapper _mapper;

        //public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        //{
        //    _repository = repository;
        //    _logger = logger;
        //    _mapper = mapper;
        //}
        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    try
        //    {
        //        var companies = _repository.Company.GetAllCompanies(trackChanges: false);
        //        return Ok(companies);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    try
        //    {
        //        var companies = _repository.Company.GetAllCompanies(trackChanges: false);
        //        var companiesDto = companies.Select(c => new CompanyDto
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            FullAddress = string.Join(' ', c.Address, c.Country)
        //        }).ToList();
        //        return Ok(companiesDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    try
        //    {
        //        var companies = _repository.Company.GetAllCompanies(trackChanges: false);
        //        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        //        return Ok(companiesDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}


        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    var companies = _repository.Company.GetAllCompanies(trackChanges: false);
        //    var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        //    return Ok(companiesDto);
        //}

        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service) => _service = service;

        //antes da implementacao do Exception handling
        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    try
        //    {
        //        var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
        //        return Ok(companies);
        //    }
        //    catch
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}


        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids, trackChanges: false);
            return Ok(companies);
        }


        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var result = _service.CompanyService.CreateCompanyCollection(companyCollection);

            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

    }
}
