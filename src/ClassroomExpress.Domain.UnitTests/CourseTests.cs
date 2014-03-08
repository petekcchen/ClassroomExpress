using NUnit.Framework;

namespace ClassroomExpress.Domain.UnitTests
{
    [TestFixture]
    public class CourseTests
    {
        private const string TeacherName = "Pete";

        [Test]
        public void Should_Convert_To_String()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            string name = "Design Patterns";
            string syllabus = "To teach you how to use Design Patterns in real-world projects";
            Course course = CourseFactory.Create(teacher, name, syllabus);
            Assert.That(course.ToString(), Is.EqualTo(string.Format("Id={0}&Name={1}&Syllabus={2}&Status={3}&Teacher={4}", course.Id, name, syllabus, CourseStatus.Open, teacher.Name)));
        }

        [Test]
        public void Should_Close_Course()
        {
            Teacher teacher = TeacherFactory.Create(TeacherName);
            string name = "Design Patterns";
            string syllabus = "To teach you how to use Design Patterns in real-world projects";
            Course course = CourseFactory.Create(teacher, name, syllabus);
            course.Close();
            Assert.That(course.Status, Is.EqualTo(CourseStatus.Close));
        }
    }
}