using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace clinicaMedicala4.Models
{
    //enum TipProgramare { Consult, Analize}
    [Table("Programari")]
    public class Programare
    {
        [Key]
        public int ProgramareId { get; set; }

        public DateTime Data { get; set; }

        [Required, RegularExpression(@"^[1-9](\d{3})$", ErrorMessage = "Anul nu este valid!")]
        public string An { get; set; }

        [Required, RegularExpression(@"^(0[1-9])|(1[012])$", ErrorMessage = "Luna nu este valida!")]
        public string Luna { get; set; }

        [Required, RegularExpression(@"^((0[1-9])|([12]\d)|(3[01]))$", ErrorMessage = "Nu este o zi valida!")]
        public string Zi { get; set; }
        
        [Required, RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Nu este o ora valida!")]
        public string Ora { get; set; }

        [MinLength(3, ErrorMessage = "Tipul programarii trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Tipul programarii nu poate sa depaseasca 50 de caractere!")]
        public string TipProgramare { get; set; }

        [MinLength(3, ErrorMessage = "Observatiile trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Observatiile nu poate sa depaseasca 50 de caractere!")]
        public string Observatii { get; set; }
        //public TipProgramare Tip { get; set; }

        // Relatie many-to-one cu Pacient
        [Column("Pacient_id")]
        [ForeignKey("Pacient")]
        public int PacientId { get; set; }
        public virtual Pacient Pacient { get; set; }

        //// Relatie many-to-one cu Filiala
        //[Column("Filiala_id")]
        //[ForeignKey("Filiala")]
        //public int FilialaId { get; set; }
        //public virtual Filiala Filiala { get; set; }
    }
}