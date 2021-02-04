using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace clinicaMedicala4.Models
{
    [Table("Departamente")]
    public class Departament
    {
        [Key]
        public int DepId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Denumirea trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Denumirea nu poate sa depaseasca 50 de caractere!")]
        public string Denumire { get; set; }

        //public string Adresa { get; set; }
        [Required]
        public int NrAngajati { get; set; }


        //Relatie many-to-one cu Filiala
        [Column("Filiala_id")]
        [ForeignKey("Filiala")]
        public int FilialaId { get; set; }
        public virtual Filiala Filiala { get; set; }

        ////Relatie one-to-many cu Doctor
        //public virtual ICollection<Doctor> Doctori { get; set; }
    }
    
}