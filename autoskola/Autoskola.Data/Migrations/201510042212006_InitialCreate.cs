namespace Autoskola.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administratoris",
                c => new
                    {
                        AdministratorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdministratorId)
                .ForeignKey("dbo.Korisnicis", t => t.AdministratorId)
                .Index(t => t.AdministratorId);
            
            CreateTable(
                "dbo.Korisnicis",
                c => new
                    {
                        KorisnikId = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        KorisnickoIme = c.String(),
                        LozinkaHash = c.String(),
                        JMBG = c.String(),
                        Telefon = c.String(),
                        Adresa = c.String(),
                        Email = c.String(),
                        Aktivan = c.Byte(nullable: false),
                        DatumRodjenja = c.DateTime(nullable: false),
                        DatumRegistracije = c.DateTime(nullable: false),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KorisnikId)
                .ForeignKey("dbo.Gradovis", t => t.GradId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Gradovis",
                c => new
                    {
                        GradId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        DrzavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradId)
                .ForeignKey("dbo.Drzaves", t => t.DrzavaId)
                .Index(t => t.DrzavaId);
            
            CreateTable(
                "dbo.AutoSkoles",
                c => new
                    {
                        AutoSkolaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        PostanskiBroj = c.String(),
                        Telefon = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AutoSkolaId)
                .ForeignKey("dbo.Gradovis", t => t.GradId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Instruktoris",
                c => new
                    {
                        InstruktorId = c.Int(nullable: false),
                        SifraLicense = c.String(),
                        KategorijeObuke = c.String(),
                        AutoSkolaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstruktorId)
                .ForeignKey("dbo.AutoSkoles", t => t.AutoSkolaId)
                .ForeignKey("dbo.Korisnicis", t => t.InstruktorId)
                .Index(t => t.InstruktorId)
                .Index(t => t.AutoSkolaId);
            
            CreateTable(
                "dbo.Prijaves",
                c => new
                    {
                        PrijavaId = c.Int(nullable: false, identity: true),
                        DatumPrijave = c.DateTime(nullable: false),
                        KategorijaPolaganja = c.String(),
                        UkupnoUplatiti = c.String(),
                        Zavrseno = c.Byte(nullable: false),
                        Status = c.Byte(nullable: false),
                        KandidatId = c.Int(nullable: false),
                        InstruktorId = c.Int(nullable: false),
                        AutoSkolaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrijavaId)
                .ForeignKey("dbo.AutoSkoles", t => t.AutoSkolaId)
                .ForeignKey("dbo.Instruktoris", t => t.InstruktorId)
                .ForeignKey("dbo.Kandidatis", t => t.KandidatId)
                .Index(t => t.KandidatId)
                .Index(t => t.InstruktorId)
                .Index(t => t.AutoSkolaId);
            
            CreateTable(
                "dbo.Kandidatis",
                c => new
                    {
                        KandidatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KandidatId)
                .ForeignKey("dbo.Korisnicis", t => t.KandidatId)
                .Index(t => t.KandidatId);
            
            CreateTable(
                "dbo.KategorijePrijaves",
                c => new
                    {
                        KategorijaPrijavaId = c.Int(nullable: false, identity: true),
                        PrijavaId = c.Int(nullable: false),
                        KategorijaId = c.Int(nullable: false),
                        Spremnost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.KategorijaPrijavaId)
                .ForeignKey("dbo.Kategorijes", t => t.KategorijaId)
                .ForeignKey("dbo.Prijaves", t => t.PrijavaId)
                .Index(t => t.PrijavaId)
                .Index(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Kategorijes",
                c => new
                    {
                        KategorijaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                        BrPitanjaTest = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KategorijaId);
            
            CreateTable(
                "dbo.BrojPitanjas",
                c => new
                    {
                        BrojPitanjaId = c.Int(nullable: false, identity: true),
                        Broj = c.Int(nullable: false),
                        GrupaPitanjaId = c.Int(nullable: false),
                        KategorijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrojPitanjaId)
                .ForeignKey("dbo.GrupePitanjas", t => t.GrupaPitanjaId)
                .ForeignKey("dbo.Kategorijes", t => t.KategorijaId)
                .Index(t => t.GrupaPitanjaId)
                .Index(t => t.KategorijaId);
            
            CreateTable(
                "dbo.GrupePitanjas",
                c => new
                    {
                        GrupaPitanjaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                        PitanjeBod = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.GrupaPitanjaId);
            
            CreateTable(
                "dbo.Pitanjas",
                c => new
                    {
                        PitanjeId = c.Int(nullable: false, identity: true),
                        Pitanje = c.String(),
                        Slika = c.String(),
                        DatumDodavanja = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                        Multichoice = c.Byte(nullable: false),
                        GrupaPitanjaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PitanjeId)
                .ForeignKey("dbo.GrupePitanjas", t => t.GrupaPitanjaId)
                .Index(t => t.GrupaPitanjaId);
            
            CreateTable(
                "dbo.OdabraniOdgovoris",
                c => new
                    {
                        OdabraniOdgovorId = c.Int(nullable: false, identity: true),
                        OdgovorId = c.Int(nullable: false),
                        PitanjeId = c.Int(nullable: false),
                        UradjeniTestId = c.Int(nullable: false),
                        Bodovi = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OdabraniOdgovorId)
                .ForeignKey("dbo.Odgovoris", t => t.OdgovorId)
                .ForeignKey("dbo.Pitanjas", t => t.PitanjeId)
                .ForeignKey("dbo.UradjeniTestovis", t => t.UradjeniTestId)
                .Index(t => t.OdgovorId)
                .Index(t => t.PitanjeId)
                .Index(t => t.UradjeniTestId);
            
            CreateTable(
                "dbo.Odgovoris",
                c => new
                    {
                        OdgovorId = c.Int(nullable: false, identity: true),
                        Odgovor = c.String(),
                        Tacan = c.Byte(nullable: false),
                        PitanjeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OdgovorId)
                .ForeignKey("dbo.Pitanjas", t => t.PitanjeId)
                .Index(t => t.PitanjeId);
            
            CreateTable(
                "dbo.UradjeniTestovis",
                c => new
                    {
                        UradjeniTestId = c.Int(nullable: false, identity: true),
                        MaxBodovi = c.Double(nullable: false),
                        OsvojeniBodovi = c.Double(nullable: false),
                        OsvojeniProcenat = c.Double(nullable: false),
                        PocetakTesta = c.DateTime(nullable: false),
                        KrajTesta = c.DateTime(nullable: false),
                        Polozeno = c.Boolean(nullable: false),
                        KategorijaPrijavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UradjeniTestId)
                .ForeignKey("dbo.KategorijePrijaves", t => t.KategorijaPrijavaId)
                .Index(t => t.KategorijaPrijavaId);
            
            CreateTable(
                "dbo.PitanjaKategorijes",
                c => new
                    {
                        PitanjaKategorijaId = c.Int(nullable: false, identity: true),
                        PitanjeId = c.Int(nullable: false),
                        KategorijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PitanjaKategorijaId)
                .ForeignKey("dbo.Kategorijes", t => t.KategorijaId)
                .ForeignKey("dbo.Pitanjas", t => t.PitanjeId)
                .Index(t => t.PitanjeId)
                .Index(t => t.KategorijaId);
            
            CreateTable(
                "dbo.PolaganjePrvePomocis",
                c => new
                    {
                        PolaganjePrvePomociId = c.Int(nullable: false, identity: true),
                        DatumPolaganja = c.DateTime(nullable: false),
                        Polozeno = c.Byte(nullable: false),
                        PrijavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PolaganjePrvePomociId)
                .ForeignKey("dbo.Prijaves", t => t.PrijavaId)
                .Index(t => t.PrijavaId);
            
            CreateTable(
                "dbo.Drzaves",
                c => new
                    {
                        DrzavaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.DrzavaId);
            
            CreateTable(
                "dbo.PolaganjeTestovas",
                c => new
                    {
                        PolaganjeTestovaId = c.Int(nullable: false, identity: true),
                        DatumPolaganja = c.DateTime(nullable: false),
                        Polozeno = c.Byte(nullable: false),
                        KategorijaPrijavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PolaganjeTestovaId)
                .ForeignKey("dbo.KategorijePrijaves", t => t.KategorijaPrijavaId)
                .Index(t => t.KategorijaPrijavaId);
            
            CreateTable(
                "dbo.PolaganjeVoznjes",
                c => new
                    {
                        PolaganjeVoznjeId = c.Int(nullable: false, identity: true),
                        DatumPolaganja = c.DateTime(nullable: false),
                        Polozeno = c.Byte(nullable: false),
                        PrijavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PolaganjeVoznjeId)
                .ForeignKey("dbo.Prijaves", t => t.PrijavaId)
                .Index(t => t.PrijavaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolaganjeVoznjes", "PrijavaId", "dbo.Prijaves");
            DropForeignKey("dbo.PolaganjeTestovas", "KategorijaPrijavaId", "dbo.KategorijePrijaves");
            DropForeignKey("dbo.Kandidatis", "KandidatId", "dbo.Korisnicis");
            DropForeignKey("dbo.Instruktoris", "InstruktorId", "dbo.Korisnicis");
            DropForeignKey("dbo.Korisnicis", "GradId", "dbo.Gradovis");
            DropForeignKey("dbo.Gradovis", "DrzavaId", "dbo.Drzaves");
            DropForeignKey("dbo.PolaganjePrvePomocis", "PrijavaId", "dbo.Prijaves");
            DropForeignKey("dbo.KategorijePrijaves", "PrijavaId", "dbo.Prijaves");
            DropForeignKey("dbo.KategorijePrijaves", "KategorijaId", "dbo.Kategorijes");
            DropForeignKey("dbo.BrojPitanjas", "KategorijaId", "dbo.Kategorijes");
            DropForeignKey("dbo.PitanjaKategorijes", "PitanjeId", "dbo.Pitanjas");
            DropForeignKey("dbo.PitanjaKategorijes", "KategorijaId", "dbo.Kategorijes");
            DropForeignKey("dbo.OdabraniOdgovoris", "UradjeniTestId", "dbo.UradjeniTestovis");
            DropForeignKey("dbo.UradjeniTestovis", "KategorijaPrijavaId", "dbo.KategorijePrijaves");
            DropForeignKey("dbo.OdabraniOdgovoris", "PitanjeId", "dbo.Pitanjas");
            DropForeignKey("dbo.Odgovoris", "PitanjeId", "dbo.Pitanjas");
            DropForeignKey("dbo.OdabraniOdgovoris", "OdgovorId", "dbo.Odgovoris");
            DropForeignKey("dbo.Pitanjas", "GrupaPitanjaId", "dbo.GrupePitanjas");
            DropForeignKey("dbo.BrojPitanjas", "GrupaPitanjaId", "dbo.GrupePitanjas");
            DropForeignKey("dbo.Prijaves", "KandidatId", "dbo.Kandidatis");
            DropForeignKey("dbo.Prijaves", "InstruktorId", "dbo.Instruktoris");
            DropForeignKey("dbo.Prijaves", "AutoSkolaId", "dbo.AutoSkoles");
            DropForeignKey("dbo.Instruktoris", "AutoSkolaId", "dbo.AutoSkoles");
            DropForeignKey("dbo.AutoSkoles", "GradId", "dbo.Gradovis");
            DropForeignKey("dbo.Administratoris", "AdministratorId", "dbo.Korisnicis");
            DropIndex("dbo.PolaganjeVoznjes", new[] { "PrijavaId" });
            DropIndex("dbo.PolaganjeTestovas", new[] { "KategorijaPrijavaId" });
            DropIndex("dbo.PolaganjePrvePomocis", new[] { "PrijavaId" });
            DropIndex("dbo.PitanjaKategorijes", new[] { "KategorijaId" });
            DropIndex("dbo.PitanjaKategorijes", new[] { "PitanjeId" });
            DropIndex("dbo.UradjeniTestovis", new[] { "KategorijaPrijavaId" });
            DropIndex("dbo.Odgovoris", new[] { "PitanjeId" });
            DropIndex("dbo.OdabraniOdgovoris", new[] { "UradjeniTestId" });
            DropIndex("dbo.OdabraniOdgovoris", new[] { "PitanjeId" });
            DropIndex("dbo.OdabraniOdgovoris", new[] { "OdgovorId" });
            DropIndex("dbo.Pitanjas", new[] { "GrupaPitanjaId" });
            DropIndex("dbo.BrojPitanjas", new[] { "KategorijaId" });
            DropIndex("dbo.BrojPitanjas", new[] { "GrupaPitanjaId" });
            DropIndex("dbo.KategorijePrijaves", new[] { "KategorijaId" });
            DropIndex("dbo.KategorijePrijaves", new[] { "PrijavaId" });
            DropIndex("dbo.Kandidatis", new[] { "KandidatId" });
            DropIndex("dbo.Prijaves", new[] { "AutoSkolaId" });
            DropIndex("dbo.Prijaves", new[] { "InstruktorId" });
            DropIndex("dbo.Prijaves", new[] { "KandidatId" });
            DropIndex("dbo.Instruktoris", new[] { "AutoSkolaId" });
            DropIndex("dbo.Instruktoris", new[] { "InstruktorId" });
            DropIndex("dbo.AutoSkoles", new[] { "GradId" });
            DropIndex("dbo.Gradovis", new[] { "DrzavaId" });
            DropIndex("dbo.Korisnicis", new[] { "GradId" });
            DropIndex("dbo.Administratoris", new[] { "AdministratorId" });
            DropTable("dbo.PolaganjeVoznjes");
            DropTable("dbo.PolaganjeTestovas");
            DropTable("dbo.Drzaves");
            DropTable("dbo.PolaganjePrvePomocis");
            DropTable("dbo.PitanjaKategorijes");
            DropTable("dbo.UradjeniTestovis");
            DropTable("dbo.Odgovoris");
            DropTable("dbo.OdabraniOdgovoris");
            DropTable("dbo.Pitanjas");
            DropTable("dbo.GrupePitanjas");
            DropTable("dbo.BrojPitanjas");
            DropTable("dbo.Kategorijes");
            DropTable("dbo.KategorijePrijaves");
            DropTable("dbo.Kandidatis");
            DropTable("dbo.Prijaves");
            DropTable("dbo.Instruktoris");
            DropTable("dbo.AutoSkoles");
            DropTable("dbo.Gradovis");
            DropTable("dbo.Korisnicis");
            DropTable("dbo.Administratoris");
        }
    }
}
