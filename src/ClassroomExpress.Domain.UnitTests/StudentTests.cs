using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ClassroomExpress.Domain.UnitTests
{
    [TestFixture]
    public class StudentTests : BaseTests
    {
        private const string TeacherName = "Claire";
        private const string StudentName = "Pete";

        [Test]
        public void Should_Convert_To_String()
        {
            Student student = StudentFactory.Create(StudentName);
            Assert.That(student.ToString(), Is.EqualTo(string.Format("Id={0}&Name={1}&CanTakeCourse={2}", student.Id, student.Name, student.CanTakeCourse)));
        }

        [Test]
        public void Should_Take_Course()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Course course = CourseFactory.Create(teacher, "Design Patterns", "To teach you how to use Design Patterns in real-world projects");

            Student student = StudentFactory.Create(StudentName);
            student.TakeCourse(course);
            Assert.That(student.CoursesTaken.Any(c => c.Name == "Design Patterns" && c.Status == CourseStatus.Open), Is.True);
        }

        [Test]
        public void Should_Throw_NoTakenCoursePermissionException()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Course course = CourseFactory.Create(teacher, "Design Patterns", "To teach you how to use Design Patterns in real-world projects");
            Student student = StudentFactory.Create(StudentName, false);

            Assert.Throws<NoTakeCoursePermissionException>(() =>
            {
                student.TakeCourse(course);
            });
        }

        [Test]
        public void Should_Throw_DuplicateCourseException_When_Taking_Existing_Course()
        {
            Student student = StudentFactory.Create(Guid.NewGuid(), StudentName, this.CreateCourses());

            Assert.Throws<DuplicateCourseException>(() =>
            {
                student.TakeCourse(student.CoursesTaken.ElementAt(0));
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Taking_Invalid_Course()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Student student = StudentFactory.Create(StudentName);
                student.TakeCourse(null);
            });
        }

        [Test]
        public void Should_Leave_Course()
        {
            IEnumerable<Course> courses = this.CreateCourses();
            Student student = StudentFactory.Create(Guid.NewGuid(), StudentName, courses);
            Course courseToBeLeft = courses.ElementAt(0);
            student.LeaveCourse(courseToBeLeft);

            Course leftCourse = student.CoursesTaken.SingleOrDefault(c => c.Id == courseToBeLeft.Id);
            Assert.That(leftCourse, Is.Null);
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Leaving_Invalid_Course()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Student student = StudentFactory.Create(StudentName);
                student.LeaveCourse(null);
            });
        }

        [Test]
        public void Should_Throw_CourseNotFoundException_When_Leaving_Nonexistent_Course()
        {
            Student student = StudentFactory.Create(Guid.NewGuid(), StudentName, this.CreateCourses());
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Course courseToBeLeft = CourseFactory.Create(teacher, "Fake course name", "Fake course syllabus");

            Assert.Throws<CourseNotFoundException>(() =>
            {
                student.LeaveCourse(courseToBeLeft);
            });
        }
    }
}