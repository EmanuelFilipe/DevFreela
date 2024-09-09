namespace DevFreela.Application.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, string fullNameClient, string fullNameFreelancer, 
                                string status,  DateTime createdAt)
        {
            Id = id;
            Title = title;
            FullNameClient = fullNameClient;
            FullNameFreelancer = fullNameFreelancer;
            Status = status;
            CreatedAt = createdAt;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string FullNameClient { get; set; }
        public string FullNameFreelancer { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
