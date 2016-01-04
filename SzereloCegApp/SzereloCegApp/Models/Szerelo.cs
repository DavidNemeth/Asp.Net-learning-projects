﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SzereloCegApp.Models
{
    public class Szerelo
    {
        //opcionális (null error refnél)
        public Szerelo()
        {
            this.Kliensek = new HashSet<Kliens>();
        }
        
        public int ID { get; set; }
        [Required(ErrorMessage = "Kérem adja meg a szerelő Vezetéknevét")]
        public string Vezetéknév { get; set; }
        [Required(ErrorMessage = "Kérem adja meg a szerelő Keresztnevét")]
        public string Keresztnév { get; set; }
        public virtual ICollection<Kliens> Kliensek { get; set; }
    }
}