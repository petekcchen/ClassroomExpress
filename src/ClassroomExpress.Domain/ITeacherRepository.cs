using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomExpress.Domain
{
    public interface ITeacherRepository
    {
        Teacher GetTeacher(Guid teacherId);
    }
}
