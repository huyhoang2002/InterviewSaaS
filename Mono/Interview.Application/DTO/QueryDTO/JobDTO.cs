using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.QueryDTO
{
    public class JobDTO
    {
        public Guid Id { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string Level { get; set; }
        public string Position { get; set; }
        public int MinExp { get; set; }
        public int MaxExp { get; set; }

    }
}
