using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheInterviewBook.Models.Internal
{
    public class InterviewBook
    {
        
        [Key]
        public Guid ID { get; set; }
	    public decimal Version { get; set; }
	    public Guid ThemeID { get; set; }

        //public List<Dictionary<int, int>> GetThemes()
        //{
        //    return new List<Dictionary<int, int>>();
        //}
    }
}
