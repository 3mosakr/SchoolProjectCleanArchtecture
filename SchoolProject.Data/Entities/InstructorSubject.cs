namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        public int InstructorId { get; set; }
        public int SubjectId { get; set; }

        public Instructor Instructor { get; set; }

        public Subject Subject { get; set; }
    }
}
