using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ClassroomExpress.Domain.UnitTests
{
    [TestFixture]
    public class TeacherFactoryTests : BaseTests
    {
        private const string TeacherName = "Pete";

        [Test]
        public void Should_Create_New_Teacher()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Assert.That(teacher, Is.Not.Null);
            Assert.That(teacher, Is.InstanceOf<User>());
            Assert.That(teacher.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(teacher.Name, Is.EqualTo(TeacherName));
            Assert.That<int>(teacher.CoursesTaught.Count, Is.EqualTo(0));
            Assert.That(teacher.CanOpenCourse, Is.EqualTo(true));
        }

        [Test]
        public void Should_Create_Existing_Teacher()
        {
            Guid teacherId = Guid.NewGuid();
            IEnumerable<Course> courses = this.CreateCourses();
            Teacher teacher = TeacherFactory.Create(teacherId, TeacherName, courses);
            Assert.That(teacher, Is.Not.Null);
            Assert.That(teacher, Is.InstanceOf<User>());
            Assert.That(teacher.Id, Is.EqualTo(teacherId));
            Assert.That(teacher.Name, Is.EqualTo(TeacherName));
            Assert.That(teacher.CoursesTaught, Is.EqualTo(courses));
            Assert.That(teacher.CanOpenCourse, Is.EqualTo(true));
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Should_Throw_ArgumentNullException_When_Creaing_New_Teacher(string teacherName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(teacherName);
            });
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_Existing_Teacher(string teacherName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(Guid.NewGuid(), teacherName, new List<Course>());
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_Existing_Teacher()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(Guid.Empty, TeacherName, new List<Course>());
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(Guid.NewGuid(), TeacherName, null);
            });
        }
    }
}