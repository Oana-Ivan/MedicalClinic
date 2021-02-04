using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace clinicaMedicala4.Models
{
    [Table("ContactPacienti")]
    public class ContactPacient
    {
        [Key]
        public int ContactPacientId { get; set; }
        public string NumarTelefon { get; set; }
        public string Adresa { get; set; }

        //Relatie one-to-one cu Pacient
        public virtual Pacient Pacient { get; set; }

    }
}