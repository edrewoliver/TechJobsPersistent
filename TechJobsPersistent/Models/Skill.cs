using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechJobsPersistent.Models
{
    public class Skill
    {
    
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public Skill()
        {
        }

        public Skill(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
