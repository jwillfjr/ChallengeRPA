using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICourseRepository courseRepository)
        {
            this.CourseRepository = courseRepository;
        }
        public ICourseRepository CourseRepository { get; }
    }
}
