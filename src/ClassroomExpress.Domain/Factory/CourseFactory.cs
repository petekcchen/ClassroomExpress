using System;
using ClassroomExpress.ExtensionMethod;

namespace ClassroomExpress.Domain
{
    public sealed class CourseFactory
    {
        public static Course Create(Teacher teacher, string name, string syllabus)
        {
            return Create(Guid.NewGuid(), teacher, name, syllabus, CourseStatus.Open, DateTimeOffset.Now);
        }

        public static Course Create(Guid id, Teacher teacher, string name, string syllabus, CourseStatus status, DateTimeOffset createdOn)
        {
            id.ThrowIfEmpty("id");
            teacher.ThrowIfNull("teacher");
            name.ThrowIfNullOrEmpty("name");
            syllabus.ThrowIfNullOrEmpty("syllabus");

            try
            {
                Course course = new Course(id, teacher, name, syllabus, status, createdOn);
                return course;
            }
            catch (Exception ex)
            {
                throw new ObjectNotCreatedException("Teacher", ex);
            }
        }
    }
}