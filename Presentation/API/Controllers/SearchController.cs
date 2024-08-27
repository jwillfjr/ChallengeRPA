using Data;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IRPA;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISearch search;
        public SearchController(IUnitOfWork unitOfWork,
            ISearch search)
        {
            this.unitOfWork = unitOfWork;
            this.search = search;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string q)
        {
            if (string.IsNullOrEmpty(q) || q.Length < 3) {
                return BadRequest("Enter at least 3 characters");
            }
            var courses = await search.GetCourses(q);
            foreach (var course in courses)
            {
                if(await unitOfWork.CourseRepository.GetByTitleAsync(course.Title) == null)
                {
                    await unitOfWork.CourseRepository.AddAsync(course);
                }
            }
            return Json(courses);
        }
    }
}
