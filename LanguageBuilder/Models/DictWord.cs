using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LanguageBuilder.Models
{
    public class DictWord
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DictWordID { get; set; }
        public string word_gender { get; set; }
        public string german_name { get; set; }
        public string english_name { get; set; }
        public string speech_type { get; set; }
        public string conjugation { get; set; }


        
        public virtual ICollection<UserWord> UserWord { get; set; }
    }
}