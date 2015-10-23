using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Autoskola.Data.Model;

namespace Autoskola.Data
{
    public class dataContext : DbContext
    {
        public dataContext()
            : base("Name=MojConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();

            

    //        modelBuilder.Entity<Voznje>()
    //.HasRequired(d => d.Instruktor)
    //.WithMany(w => w.Voznje)
    //.HasForeignKey(d => d.instruktorId)
    //.WillCascadeOnDelete(false);

   //         modelBuilder.Entity<Prijave>()
   //.HasRequired(d => d.AutoSkola)
   //.WithMany(w => w.Prijave)
   //.HasForeignKey(d => d.AutoSkolaId)
   //.WillCascadeOnDelete(false);

            

            modelBuilder.Entity<Instruktori>()
   .HasRequired(d => d.AutoSkola)
   .WithMany(w => w.Instruktori)
   .HasForeignKey(d => d.AutoSkolaId)
   .WillCascadeOnDelete(false);

            /////////

            //        modelBuilder.Entity<Prijave>()
            //.HasRequired(t => t.AutoSkola)
            //.WithMany(t => t.Prijave)
            //.HasForeignKey(d => d.AutoSkolaId)
            //.WillCascadeOnDelete(false);

            //        modelBuilder.Entity<Voznje>()
            // .HasRequired(t => t.Prijava)
            // .WithMany(t => t.Voznje)
            // .HasForeignKey(d => d.PrijavaId)
            // .WillCascadeOnDelete(false);

            /// 
            //modelBuilder.Entity<Korisnici>()
            //   .HasOptional(e => e.Ucenici)
            //   .WithRequired(e => e.Korisnici)
            //   .WillCascadeOnDelete();
            //

            //one-to-(zero or one)
            modelBuilder.Entity<Korisnici>().HasOptional(x => x.Administrator).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnici>().HasOptional(x => x.Kandidat).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnici>().HasOptional(x => x.Instruktor).WithRequired(x => x.Korisnik);
            //many-to-one
            //modelBuilder.Entity<Smjer>().HasRequired(x => x.Fakultet).WithMany().HasForeignKey(x=>x.FakultetId);
            //modelBuilder.Entity<UpisGodine>().HasRequired(x => x.Student).WithMany().HasForeignKey(x=>x.StudentId);
            //modelBuilder.Entity<UpisGodine>().HasRequired(x => x.AkademskaGodina).WithMany().HasForeignKey(x=>x.AkademskaGodinaId);
            //http://blogs.msdn.com/b/adonet/archive/2010/12/14/ef-feature-ctp5-fluent-api-samples.aspx
        }

        public DbSet<AutoSkole> AutoSkole { get; set; }
        public DbSet<Administratori> Administratori { get; set; }
        public DbSet<BrojPitanja> BrojPitanja { get; set; }
        public DbSet<Drzave> Drzave { get; set; }
        public DbSet<Gradovi> Gradovi { get; set; }
        public DbSet<GrupePitanja> GrupePitanja { get; set; }
        public DbSet<Instruktori> Instruktori { get; set; }
        public DbSet<Kandidati> Kandidati { get; set; }
        public DbSet<KategorijePrijave> KategorijePrijave { get; set; }
        public DbSet<Kategorije> Kategorije { get; set; }

        public DbSet<PitanjaKategorije> PitanjaKategorije { get; set; }

        public DbSet<Korisnici> Korisnici { get; set; }
        public DbSet<OdabraniOdgovori> OdabraniOdgovori { get; set; }
        public DbSet<Odgovori> Odgovori { get; set; }
        public DbSet<Pitanja> Pitanja { get; set; }
        public DbSet<PolaganjePrvePomoci> PolaganjePrvePomoci { get; set; }

        public DbSet<PolaganjeTestova> PolaganjeTestova { get; set; }
        public DbSet<PolaganjeVoznje> PolaganjeVoznje { get; set; }
        public DbSet<Prijave> Prijave { get; set; }
        public DbSet<UradjeniTestovi> UradjeniTestovi { get; set; }

    }
}
