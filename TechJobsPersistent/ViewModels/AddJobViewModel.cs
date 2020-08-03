﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public List<Skill> Skills { get; set; }
        [Required]
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set; }
        public List<int> SkillId { get; set; }

        public AddJobViewModel(List<Employer> employers, List<Skill> skills) 
        {
            Skills = skills;
            Employers = new List<SelectListItem>();

            foreach(var employer in employers)
            {
                Employers.Add(
                    new SelectListItem
                    {
                        Value = employer.Id.ToString(),
                        Text =employer.Name
                    }
                ); ;
            }

        }
        public AddJobViewModel () { }

    }
}
