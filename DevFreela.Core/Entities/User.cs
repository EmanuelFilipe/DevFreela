using System;
using System.Collections.Generic;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            
            Active = true;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<UserSkill> Skills { get; private set; } = [];
        public bool Active { get; set; }

        // where the user acting with a client
        public List<Project> OwnedProjects { get; private set; } = [];
        // where the user acting with a freelancer
        public List<Project> FreelanceProjects { get; private set; } = [];
        public List<ProjectComment> Comments { get; private set; }

        public void Update(string fullName, string email, DateTime bithDate, bool active)
        {
            FullName = fullName;
            Email = email;
            BirthDate= bithDate;
            Active = active;
        }
    }
}
