using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Companies
{
    public class Company : EntityBase<Guid>
    {
        public string CompanyName { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyDomain { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string Email { get; set; }
        private readonly List<Address> companyAddresses = new List<Address>();
        public IReadOnlyCollection<Address> CompanyAddresses => companyAddresses;

        private readonly List<JobCategory> jobCategories = new List<JobCategory>();
        public IReadOnlyCollection<JobCategory> JobCategories => jobCategories; 

        private readonly List<Job> jobs = new List<Job>();
        public IReadOnlyCollection<Job> Jobs => jobs;

        private readonly List<Rating> ratings = new List<Rating>();
        public IReadOnlyCollection<Rating> Ratings => ratings;

        private readonly List<Review> reviews = new List<Review>();
        public IReadOnlyCollection<Review> Reviews => reviews;


        public Company(string companyName, string companyLogoUrl, string companyDescription, string companyDomain, string companyPhoneNumber, string email)
        {
            CompanyName = companyName;
            CompanyLogoUrl = companyLogoUrl;
            CompanyDescription = companyDescription;
            CompanyDomain = companyDomain;
            CompanyPhoneNumber = companyPhoneNumber;
            Email = email;
        }
        public void AddAddress(Address address)
        {
            companyAddresses.Add(address);
        }

        public void CreateJobCategory(string categoryName)
        {
            jobCategories.Add(new JobCategory(categoryName, Id));
        }

        public bool IsJobCategoryCreated(string jobCategoryName)
        {
            var jobCategory = jobCategories.FirstOrDefault(_ => _.CategoryName == jobCategoryName);
            return jobCategory is not null;
        }

        public JobCategory FindJobCategory(Guid jobCategoryId)
        {
            var jobcategory = jobCategories.FirstOrDefault(_ => _.Id == jobCategoryId);
            return jobcategory;
        }

        public void AddJob(Job job)
        {
            jobs.Add(new Job(job));
        }
    }
}
