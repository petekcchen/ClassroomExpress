using System;
using System.Collections.Generic;
using System.Linq;
using ClassroomExpress.ExtensionMethod;

namespace ClassroomExpress.Domain
{
    public sealed class Teacher : User, IEntity<Guid>
    {
        private List<Course> _coursesTaught;

        internal Teacher(Guid id, string name, IEnumerable<Course> coursesTaught, bool allowOpenCourse = true)
        {
            this.Id = id;
            this.Name = name;
            this._coursesTaught = coursesTaught.ToList();
            this.CanOpenCourse = allowOpenCourse;
        }

        public bool CanOpenCourse { get; private set; }

        public IEnumerable<Course> CoursesTaught
        {
            get
            {
                return this._coursesTaught;
            }
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public void CloseCourse(Course course)
        {
            course.ThrowIfNull("course");

            Course courseToBeClosed = this._coursesTaught.SingleOrDefault(c => c.Id == course.Id);

            if (courseToBeClosed.IsNull())
            {
                throw new CourseNotFoundException(course.ToString());
            }

            courseToBeClosed.Close();
        }

        public void OpenCourse(Course course)
        {
            course.ThrowIfNull("course");

            if (this.CannotOpenCourse())
            {
                throw new NoOpenCoursePermissionException(this.ToString());
            }

            if (this.CourseExists(course))
            {
                throw new DuplicateCourseException(course.ToString());
            }

            this._coursesTaught.Add(course);
        }

        public override string ToString()
        {
            return string.Format(
                "Id={0}&Name={1}&CanOpenCourse={2}",
                this.Id,
                this.Name,
                this.CanOpenCourse);
        }

        private bool CannotOpenCourse()
        {
            return !this.CanOpenCourse;
        }

        private bool CourseExists(Course course)
        {
            return this._coursesTaught.SingleOrDefault(c => c.Id == course.Id) != null;
        }
    }
}