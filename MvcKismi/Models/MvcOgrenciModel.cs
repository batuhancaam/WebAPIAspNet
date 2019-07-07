using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcKismi.Models
{
    public class MvcOgrenciModel
    {

        public int OgrenciId { get; set; }
        [Required (ErrorMessage ="Burası bos olamaz!")]
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Bolum { get; set; }
        public Nullable<int> GirisYili { get; set; }
    }
}