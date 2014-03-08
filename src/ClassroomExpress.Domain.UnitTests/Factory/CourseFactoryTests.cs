using System;
using NUnit.Framework;

namespace ClassroomExpress.Domain.UnitTests
{
    [TestFixture]
    public class CourseFactoryTests
    {
        private const string TeacherName = "Pete";

        [Test]
        public void Should_Create_New_Course()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            string name = "Design Patterns";
            string syllabus = "To teach you how to use Design Patterns in real-world projects";

            Course course = CourseFactory.Create(teacher, name, syllabus);
            Assert.That(course.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(course.Name, Is.EqualTo(name));
            Assert.That(course.Syllabus, Is.EqualTo(syllabus));
            Assert.That(course.CreatedOn, Is.InstanceOf<DateTimeOffset>());
            Assert.That(course.Teacher, Is.EqualTo(teacher));
            Assert.That(course.Status, Is.EqualTo(CourseStatus.Open));
        }

        [Test]
        public void Should_Create_Existing_Course()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            Guid courseId = Guid.NewGuid();
            string name = "Design Patterns";
            string syllabus = "To teach you how to use Design Patterns in real-world projects";
            CourseStatus status = CourseStatus.Open;
            DateTimeOffset createdOn = DateTimeOffset.Now;

            Course course = CourseFactory.Create(courseId, teacher, name, syllabus, status, createdOn);
            Assert.That(course.Id, Is.EqualTo(courseId));
            Assert.That(course.Name, Is.EqualTo(name));
            Assert.That(course.Syllabus, Is.EqualTo(syllabus));
            Assert.That(course.CreatedOn, Is.EqualTo(createdOn));
            Assert.That(course.Teacher, Is.EqualTo(teacher));
            Assert.That(course.Status, Is.EqualTo(status));
        }

        [TestCase("", "To teach you how to...")]
        [TestCase(null, "To teach you how to...")]
        [TestCase("Design Patterns", "")]
        [TestCase("Design Patterns", null)]
        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_New_Course(string name, string syllabus)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(TeacherName);
                Course course = CourseFactory.Create(teacher, name, syllabus);
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_New_Course()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Course course = CourseFactory.Create(null, "Design Patterns", "To teach you how to...");
            });
        }

        [TestCase("", "To teach you how to...")]
        [TestCase(null, "To teach you how to...")]
        [TestCase("Design Patterns", "")]
        [TestCase("Design Patterns", null)]
        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_Existing_Course(string name, string syllabus)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(TeacherName);
                Course course = CourseFactory.Create(Guid.NewGuid(), teacher, name, syllabus, CourseStatus.Open, DateTimeOffset.Now);
            });
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_Creating_Existing_Course()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(TeacherName);
                Course course = CourseFactory.Create(Guid.Empty, teacher, "Fake course name", "Fake course syllabus", CourseStatus.Open, DateTimeOffset.Now);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                Teacher teacher = TeacherFactory.Create(TeacherName);
                Course course = CourseFactory.Create(Guid.NewGuid(), null, "Fake course name", "Fake course syllabus", CourseStatus.Open, DateTimeOffset.Now);
            });
        }
    }
}