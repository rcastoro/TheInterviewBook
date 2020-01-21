using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheInterviewBook.Models.Internal
{
    public class InterviewBookUser
    {
        [Key]
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
	    public string HockeyTeamID { get; set; }
	    public string PepsiOrCoke { get; set; }
    }
}
