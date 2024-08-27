using Domain.Entities;
using Domain.IRepositories;
using Domain.IRPA;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISearch search;
        public CoursesController(IUnitOfWork unitOfWork,
            ISearch search)
        {
            this.unitOfWork = unitOfWork;
            this.search = search;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            return await unitOfWork.CourseRepository.GetAllAsync();
        }
        
    }
}
