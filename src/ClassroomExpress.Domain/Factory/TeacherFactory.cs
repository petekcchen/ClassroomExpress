using System;
using System.Collections.Generic;
using ClassroomExpress.ExtensionMethod;

namespace ClassroomExpress.Domain
{
    public sealed class TeacherFactory
    {
        public static Teacher Create(string name, bool allowOpenCourse = true)
        {
            return Create(Guid.NewGuid(), name, new List<Course>(), allowOpenCourse);
        }

        public static Teacher Create(Guid id, string name, IEnumerable<Course> coursesTaught, bool allowOpenCourse = true)
        {
            id.ThrowIfEmpty("id");
            name.ThrowIfNullOrEmpty("name");
            coursesTaught.ThrowIfNull("coursesTaught");

            try
            {
                Teacher teacher = new Teacher(id, name, coursesTaught, allowOpenCourse);
                return teacher;
            }
            catch (Exception ex)
            {
                throw new ObjectNotCreatedException("Teacher", ex);
            }
        }
    }
}