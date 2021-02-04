using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace clinicaMedicala4.Models
{
    [Table("Pacienti")]
    public class Pacient
    {
        [Key]
        public int PacientId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Acest camp trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Nume { get; set; }

        [Required]
        public int Varsta { get; set; }

        //public string Adresa { get; set; }

        [MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Diagnostic { get; set; }

        [MaxLength(150, ErrorMessage = "Acest camp nu poate sa depaseasca 150 de caractere!")]
        public string Observatii { get; set; }

        [MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Istoric { get; set; }

        // Relatie many-to-many cu Doctor
        public virtual ICollection<Doctor> Doctori { get; set; }
        //public virtual List<Doctor> Doctori { get; set; }

        // Relatie one-to-one cu ContactPacient
        [Required]
        public virtual ContactPacient ContactPacient { get; set; }

        // Relatie one-to-many cu Programari
        public virtual ICollection<Programare> Programari { get; set; }

        // checkboxes list
        //[NotMapped]
        //public List<CheckBoxViewModel> ListaDoctori { get; set; }
    }
}