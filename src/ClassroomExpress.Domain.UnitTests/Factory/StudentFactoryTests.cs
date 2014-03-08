using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ClassroomExpress.Domain.UnitTests
{
    [TestFixture]
    public class StudentFactoryTests : BaseTests
    {
        private const string StudentName = "Pete";

        [Test]
        public void Should_Create_New_Student()
        {
            Student student = StudentFactory.Create(StudentName);
            Assert.That(student, Is.Not.Null);
            Assert.That(student, Is.InstanceOf<User>());
            Assert.That(student.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(student.Name, Is.EqualTo(StudentName));
            Assert.That<int>(student.CoursesTaken.Count, Is.EqualTo(0));
            Assert.That(student.CanTakeCourse, Is.EqualTo(true));
        }

        [Test]
        public void Should_Create_Existing_Student()
        {
            Guid studentId = Guid.NewGuid();
            IEnumerable<Course> courses = this.CreateCourses();
            Student student = StudentFactory.Create(studentId, StudentName, courses);
            Assert.That(student, Is.Not.Null);
            Assert.That(student, Is.InstanceOf<User>());
            Assert.That(student.Id, Is.EqualTo(studentId));
            Assert.That(student.Name, Is.EqualTo(StudentName));
            Assert.That(student.CoursesTaken, Is.EqualTo(courses));
            Assert.That(student.CanTakeCourse, Is.EqualTo(true));
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Should_Throw_ArgumentNullException_When_Creaing_New_Student(string studentName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Student student = StudentFactory.Create(studentName);
            });
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_Existing_Student(string studentName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Student student = StudentFactory.Create(Guid.NewGuid(), studentName, new List<Course>());
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_Existing_Student()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Student student = StudentFactory.Create(Guid.Empty, StudentName, new List<Course>());
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                Student student = StudentFactory.Create(Guid.NewGuid(), StudentName, null);
            });
        }
    }
}