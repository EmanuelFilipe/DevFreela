namespace DevFreela.Application.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, int idClient, string fullNameClient, int idFreelancer,
                                string fullNameFreelancer, string status,  DateTime createdAt)
        {
            Id = id;
            Title = title;
            IdClient = idClient;
            FullNameClient = fullNameClient;
            IdFreelancer = idFreelancer;
            FullNameFreelancer = fullNameFreelancer;
            Status = status;
            CreatedAt = createdAt;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public int IdClient { get; private set; }
        public string FullNameClient { get; set; }
        public int IdFreelancer { get; private set; }
        public string FullNameFreelancer { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; private set; }
    }
}
