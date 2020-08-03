using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Skill> skills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();
            AddJobViewModel addJobView = new AddJobViewModel(employers,skills);
            return View(addJobView);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string [] selectedSkills)
        {
            

            if(ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = context.Employers.Find(addJobViewModel.EmployerId)
                };
                for(int i=0; i<selectedSkills.Length; i++)
                {
                    JobSkill jobSkill = new JobSkill
                    {
                        SkillId = Int32.Parse(selectedSkills[i]),
                        JobId = newJob.Id,
                        Job= newJob,
                        Skill=context.Skills.Find(Int32.Parse(selectedSkills[i]))
                    };
                    context.JobSkills.Add(jobSkill);
                }
                
                context.Jobs.Add(newJob);

                context.SaveChanges();

                return Redirect("/Add");
            }
            List<Skill> skills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();
            AddJobViewModel rainbow = new AddJobViewModel(employers, skills);
            addJobViewModel.Employers = rainbow.Employers;
            addJobViewModel.Skills = rainbow.Skills;
            return View("AddJob",addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
