using System;

namespace ClassroomExpress.Domain
{
    public class Course : IEntity<Guid>
    {
        internal Course(Guid id, Teacher teacher, string name, string syllabus, CourseStatus status, DateTimeOffset createdOn)
        {
            this.Id = id;
            this.Teacher = teacher;
            this.Name = name;
            this.Syllabus = syllabus;
            this.Status = status;
            this.CreatedOn = createdOn;
        }

        public DateTimeOffset CreatedOn { get; private set; }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public CourseStatus Status { get; private set; }

        public string Syllabus { get; private set; }

        public Teacher Teacher { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Id={0}&Name={1}&Syllabus={2}&Status={3}&Teacher={4}",
                this.Id,
                this.Name,
                this.Syllabus,
                this.Status,
                this.Teacher.Name);
        }

        public void Close()
        {
            this.Status = CourseStatus.Close;
        }
    }
}