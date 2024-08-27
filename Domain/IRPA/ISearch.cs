using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRPA
{
    public interface ISearch:IDisposable
    {
        public  Task<Course[]> GetCourses(string term);
    }
}
