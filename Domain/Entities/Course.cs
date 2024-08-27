namespace Domain.Entities
{
    public class Course
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Teacher { get; set; }
        public required string Workload { get; set; }

    }
}
