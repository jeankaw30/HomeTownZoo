﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeTownZoo.Models
{
    public class Animal
    {
        [Key]
        public int AnimalID { get; set; }

        //[Required]
        //[StringLength(100)]
        //[Column(TypeName = "varchar")]
        public string AnimalName { get; set; }
    }
}