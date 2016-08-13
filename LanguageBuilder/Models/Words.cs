using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LanguageBuilder.Models
{
    public class Words
    {
        [Key]
        public int ID { get; set; }

        public string word_gender { get; set; }

        public string conjugation { get; set; }

        public string speech_part { get; set; }

        [Required]
        public string german_name { get; set; }

        [Required]
        public string english_name { get; set; }

       

    }
}