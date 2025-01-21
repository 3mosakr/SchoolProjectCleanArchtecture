namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal? Grade { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
