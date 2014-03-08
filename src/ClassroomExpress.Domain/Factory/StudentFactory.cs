using System;
using System.Collections.Generic;
using ClassroomExpress.ExtensionMethod;

namespace ClassroomExpress.Domain
{
    public sealed class StudentFactory
    {
        public static Student Create(string name, bool allowTakeCourse = true)
        {
            return Create(Guid.NewGuid(), name, new List<Course>(), allowTakeCourse);
        }

        public static Student Create(Guid id, string name, IEnumerable<Course> coursesTaken, bool allowTakeCourse = true)
        {
            id.ThrowIfEmpty("id");
            name.ThrowIfNullOrEmpty("name");
            coursesTaken.ThrowIfNull("coursesTaken");

            try
            {
                Student student = new Student(id, name, coursesTaken, allowTakeCourse);
                return student;
            }
            catch (Exception ex)
            {
                throw new ObjectNotCreatedException("Student", ex);
            }
        }
    }
}