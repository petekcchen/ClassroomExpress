using System.Collections.Generic;

namespace ClassroomExpress.Domain.UnitTests
{
    public class BaseTests
    {
        protected IEnumerable<Course> CreateCourses()
        {
            Teacher teacher = TeacherFactory.Create("Pete");
            Course designPatterns = CourseFactory.Create(teacher, "Design Patterns in .NET", "To teach you how to use design patterns in .NET");
            Course dependencyInjection = CourseFactory.Create(teacher, "Dependecy Injection in .NET", "To teach you how to use dependency injection in .NET");
            Course aop = CourseFactory.Create(teacher, "AOP in .NET", "To teach you how to use AOP in .NET");
            Course cleanCode = CourseFactory.Create(teacher, "Clean Code", "To teach you how to write clean code");
            List<Course> courses = new List<Course>() { designPatterns, dependencyInjection, aop };
            return courses;
        }
    }
}