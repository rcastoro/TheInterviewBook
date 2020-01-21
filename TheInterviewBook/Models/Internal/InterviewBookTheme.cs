using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheInterviewBook.Models.Internal
{
    public class InterviewBookTheme
    {
        [Key]
        public static Guid ID { get; set; }
        public static string ThemeName { get; set; }
        public static int FontColor { get; set; }
        public static int BackgroundColor { get; set; }
    }
}
