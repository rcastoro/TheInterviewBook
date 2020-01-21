using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheInterviewBook.Models.Internal
{
    public class InterviewBookContext : DbContext
    {
        public InterviewBookContext(DbContextOptions<InterviewBookContext> options)
            :base(options)
        {

        }

        public DbSet<InterviewBook> InterviewBook { get; set; }
        //public DbSet<InterviewBookTheme> Theme { get; set; }
        public DbSet<InterviewBookUser> InterviewBookUser { get; set; }
    }
}
