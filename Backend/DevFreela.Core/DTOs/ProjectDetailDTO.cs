namespace DevFreela.Core.DTOs
{
    public class ProjectDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string Status { get; set; }
        public int IdClient { get; set; }
        public string ClientFullName { get; set; }
        public int IdFreelancer { get; set; }
        public string FreelancerFullName { get; set; }
    }
}
