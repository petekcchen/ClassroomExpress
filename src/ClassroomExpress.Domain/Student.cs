using System;
using System.Collections.Generic;
using System.Linq;
using ClassroomExpress.ExtensionMethod;

namespace ClassroomExpress.Domain
{
    public class Student : User, IEntity<Guid>
    {
        private List<Course> _coursesTaken;

        internal Student(Guid id, string name, IEnumerable<Course> coursesTaken, bool allowTakenCourse = true)
        {
            this.Id = id;
            this.Name = name;
            this._coursesTaken = coursesTaken.ToList();
            this.CanTakeCourse = allowTakenCourse;
        }

        public bool CanTakeCourse { get; private set; }

        public IEnumerable<Course> CoursesTaken
        {
            get
            {
                return this._coursesTaken;
            }
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public void LeaveCourse(Course course)
        {
            course.ThrowIfNull("course");

            Course courseToBeLeft = this._coursesTaken.SingleOrDefault(c => c.Id == course.Id);

            if (courseToBeLeft.IsNull())
            {
                throw new CourseNotFoundException(course.ToString());
            }

            this._coursesTaken.Remove(course);
        }

        public void TakeCourse(Course course)
        {
            course.ThrowIfNull("course");

            if (this.CannotTakeCourse())
            {
                throw new NoTakeCoursePermissionException(this.ToString());
            }

            if (this.CourseExists(course))
            {
                throw new DuplicateCourseException(course.ToString());
            }

            this._coursesTaken.Add(course);
        }

        public override string ToString()
        {
            return string.Format(
                "Id={0}&Name={1}&CanTakeCourse={2}",
                this.Id,
                this.Name,
                this.CanTakeCourse);
        }

        private bool CannotTakeCourse()
        {
            return !this.CanTakeCourse;
        }

        private bool CourseExists(Course course)
        {
            return this._coursesTaken.SingleOrDefault(c => c.Id == course.Id) != null;
        }
    }
}