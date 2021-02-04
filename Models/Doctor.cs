using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace clinicaMedicala4.Models
{
    [Table("Doctori")]
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Acest camp trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Nume { get; set; }

        public int Varsta { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Acest camp trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Adresa { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Acest camp trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Specializare { get; set; }

        [Required]
        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "Numarul de telefon nu este valid!")]
        public string NrTelefon { get; set; }

        // Relatie many-to-many cu Pacient
        public virtual ICollection<Pacient> Pacienti { get; set; }

        //// Relatie many-to-one cu Departament
        //[Column("Departament_id")]
        //[ForeignKey("Departament")]
        //public int DepartamentId { get; set; }
        //public virtual Departament Departament { get; set; }

    }
}