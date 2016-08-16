using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LanguageBuilder.Models
{
    public class Student
    {
            //???
            //[Key, ForeignKey("User")]
            public string UserCrossID { get; set; }
            public int ID { get; set; }
            public string LastName { get; set; }
            public string FirstMidName { get; set; }
            public DateTime EnrollmentDate { get; set; }

            public virtual ICollection<UserWord> UserWord { get; set; }
            public virtual ApplicationUser User { get; set; }
        
    }
}