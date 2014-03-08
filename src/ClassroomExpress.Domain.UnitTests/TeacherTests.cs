using System;
using System.Linq;
using NUnit.Framework;

namespace ClassroomExpress.Domain.UnitTests
{
    [TestFixture]
    public class TeacherTests : BaseTests
    {
        private const string TeacherName = "Pete";

        [Test]
        public void Should_Convert_To_String()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Assert.That(teacher.ToString(), Is.EqualTo(string.Format("Id={0}&Name={1}&CanOpenCourse={2}", teacher.Id, teacher.Name, teacher.CanOpenCourse)));
        }

        [Test]
        public void Should_Open_Course()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Course course = CourseFactory.Create(teacher, "Design Patterns", "To teach you how to use Design Patterns in real-world projects");
            teacher.OpenCourse(course);
            Assert.That(teacher.CoursesTaught.Any(c => c.Name == "Design Patterns" && c.Status == CourseStatus.Open), Is.True);
        }

        [Test]
        public void Should_Throw_NoOpenCoursePermissionException()
        {
            Teacher teacher = TeacherFactory.Create("Pete", false);
            Course course = CourseFactory.Create(teacher, "Design Patterns", "To teach you how to use Design Patterns in real-world projects");

            Assert.Throws<NoOpenCoursePermissionException>(() =>
            {
                teacher.OpenCourse(course);
            });
        }

        [Test]
        public void Should_Throw_DuplicateCourseException_When_Opening_Existing_Course()
        {
            Teacher teacher = TeacherFactory.Create(Guid.NewGuid(), "Pete", this.CreateCourses());

            Assert.Throws<DuplicateCourseException>(() =>
            {
                teacher.OpenCourse(teacher.CoursesTaught.ElementAt(0));
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Opening_Invalid_Course()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(TeacherName);
                teacher.OpenCourse(null);
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Closing_Invalid_Course()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(TeacherName);
                teacher.CloseCourse(null);
            });
        }

        [Test]
        public void Should_Close_Course()
        {
            Teacher teacher = TeacherFactory.Create(Guid.NewGuid(), TeacherName, this.CreateCourses());
            Course courseToBeClosed = teacher.CoursesTaught.ElementAt(1);
            teacher.CloseCourse(courseToBeClosed);

            Course closedCourse = teacher.CoursesTaught.SingleOrDefault(c => c.Id == courseToBeClosed.Id);
            Assert.That(closedCourse, Is.Not.Null);
            Assert.That(closedCourse.Status, Is.EqualTo(CourseStatus.Close));
        }

        [Test]
        public void Should_Throw_CourseNotFoundException()
        {
            Teacher teacher = TeacherFactory.Create(Guid.NewGuid(), TeacherName, this.CreateCourses());
            Course courseToBeClosed = CourseFactory.Create(teacher, "Fake course name", "Fake course syllabus");

            Assert.Throws<CourseNotFoundException>(() =>
            {
                teacher.CloseCourse(courseToBeClosed);
            });
        }
    }
}