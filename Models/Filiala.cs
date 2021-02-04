using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace clinicaMedicala4.Models
{
    [Table("Filiale")]
    public class Filiala
    {
        [Key]
        public int FilialaId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Acest camp trebuie sa aiba minim 3 caractere!"),
        MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Denumire { get; set; }

        [Required, MinLength(10, ErrorMessage = "Adresa trebuie sa aiba minim 10 caractere"), MaxLength(50, ErrorMessage = "Acest camp nu poate sa depaseasca 50 de caractere!")]
        public string Adresa { get; set; }

        public bool Sediu { get; set; }

        [MinLength(3, ErrorMessage = "Acest camp trebuie sa aiba minim 3 caractere!"),
        MaxLength(150, ErrorMessage = "Acest camp nu poate sa depaseasca 150 de caractere!")]
        public string DetaliiProgram { get; set; }

        // Relatie one-to-many cu Departament
        public virtual ICollection<Departament> Departamente { get; set; }

        //// Relatie one-to-many cu Programare
        //public virtual ICollection<Programare> Programari { get; set; }
    }

    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }
        public DbSet<Filiala> Filiale { get; set; }
        public DbSet<Programare> Programari { get; set; }
        public DbSet<Departament> Departamente { get; set; }
        public DbSet<Doctor> Doctori { get; set; }
        public DbSet<Pacient> Pacienti { get; set; }
        public DbSet<ContactPacient> ContactPacienti { get; set;  }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }
    }
    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx ctx)
        {

            Pacient p1 = new Pacient { Nume = "Vasile Mihai", Varsta = 67, ContactPacient = new ContactPacient { Adresa = "Marte", NumarTelefon = "0767335211" } };
            Pacient p2 = new Pacient { Nume = "Popescu Adela", Varsta = 49, ContactPacient = new ContactPacient { Adresa = "Pluto", NumarTelefon = "0767335211" } };

            ctx.Pacienti.Add(p1);
            ctx.Pacienti.Add(p2);

            Departament d1 = new Departament { Denumire = "Resurse umane", NrAngajati = 6 };
            Departament d2 = new Departament { Denumire = "Kinetoterapie", NrAngajati = 19 };
            Departament d3 = new Departament { Denumire = "Kinetoterapie", NrAngajati = 5 };
            Departament d4 = new Departament { Denumire = "Fizioterapie", NrAngajati = 7 };

            ctx.Departamente.Add(d1);
            ctx.Departamente.Add(d2);
            ctx.Departamente.Add(d3);
            ctx.Departamente.Add(d4);

            Filiala f1 = new Filiala { 
                Denumire = "Filiala 1", 
                Adresa = "Bucuresti, Bd Iuliu Maniu, nr 30", 
                Sediu = true, 
                DetaliiProgram = "Acest punct de lucru este deschis in urmatoarele intervale:\n - luni pana miercuri intre orele 9:00 - 14:00\n - joi - vineri intre orele 14:00 - 18:00 ",
                Departamente = new List<Departament> { d1, d2},
                //Programari = new List<Programare> { new Programare { Data = DateTime.Parse("02.02.2021"), TipProgramare = "consultatie", PacientId = p1.PacientId },
                //                                    new Programare { Data = DateTime.Parse("07.02.2021"), TipProgramare = "consultatie" , PacientId = p2.PacientId }}
            };
            Filiala f2 = new Filiala
            {
                Denumire = "Filiala 2",
                Adresa = "Bucuresti, Bd Regina Elisabeta, nr 30",
                DetaliiProgram = "Acest punct de lucru este deschis in urmatoarele intervale: luni pana miercuri intre orele 9:00 - 14:00 si joi - vineri intre orele 14:00 - 18:00 ",
                Departamente = new List<Departament> { d3, d4}
            };

            ctx.Filiale.Add(f1);
            ctx.Filiale.Add(f2);

            Doctor doc1 = new Doctor { Nume = "Popescu Ion", Varsta = 45, Adresa = "Bucuresti, str. Abc, nr 13", Specializare = "kinetoterapie", NrTelefon = "0734611433"};
            Doctor doc2 = new Doctor { Nume = "Vasilescu Daniel", Varsta = 35, Adresa = "Bucuresti, str. Xyz, nr 17", Specializare = "kinetoterapie", NrTelefon = "0734611433" }; //, DepartamentId = d2.DepId };
            Doctor doc3 = new Doctor { Nume = "Dumitru Maria", Varsta = 30, Adresa = "Bucuresti, str. Abc, nr 89", Specializare = "fizioterapie", NrTelefon = "0734611433" }; //, DepartamentId = d4.DepId };

            ctx.Doctori.Add(doc1);
            ctx.Doctori.Add(doc2);
            ctx.Doctori.Add(doc3);

            //Programare prog1 = new Programare { Data = DateTime.Parse("02.02.2021"), TipProgramare = "consultatie", Observatii = "Pacientul are afectiuni cardiace", PacientId = p1.PacientId }; //, FilialaId = f1.FilialaId };
            //Programare prog2 = new Programare { Data = DateTime.Parse("07.02.2021"), TipProgramare = "consultatie", Observatii = "Pacientul nu are afectiuni cardiace", PacientId = p2.PacientId }; //, FilialaId = f2.FilialaId };

            //ctx.Programari.Add(prog1);
            //ctx.Programari.Add(prog2);

            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
}