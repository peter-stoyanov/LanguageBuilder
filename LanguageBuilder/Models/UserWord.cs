using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageBuilder.Models
{
   
       

        public class UserWord
        {
            public int UserWordID { get; set; }
            public int DictWordID { get; set; }
            public int StudentID { get; set; }
            public int Level { get; set; }
            public DateTime LastReview { get; set; }
            public DateTime NextReview { get; set; }



            public virtual DictWord DictWord { get; set; }
            public virtual Student Student { get; set; }
        }
    
}