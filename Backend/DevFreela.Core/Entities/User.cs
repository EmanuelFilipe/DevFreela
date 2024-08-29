using System;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, string password, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Password = password;
            Role = role;
            Active = true;
        }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<UserSkill> Skills { get; private set; } = [];
        public bool Active { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // where the user acting with a client
        public List<Project> OwnedProjects { get; private set; } = [];
        // where the user acting with a freelancer
        public List<Project> FreelanceProjects { get; private set; } = [];
        public List<ProjectComment> Comments { get; private set; }

        public void Update(int id, string fullName, string email, DateTime bithDate, string role, bool active)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = bithDate;
            Role = role;
            Active = active;
        }

        public void Delete()
        {
            Active = !Active;
        }
    }
}
