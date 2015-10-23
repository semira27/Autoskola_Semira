using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Autoskola.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterRoutes(RouteCollection routeCollection)
        {
            /*------------Kandidat-------------*/

            routeCollection.MapPageRoute("Kandidat-Naslovnica", "kandidat/naslovnica", "~/forms/kandidat/index.aspx");
            routeCollection.MapPageRoute("Prijava", "prijava", "~/forms/shared/login.aspx");
            routeCollection.MapPageRoute("Kandidat-PrijavaId", "kandidat/prijavljene-kategorije", "~/forms/kandidat/kategorije.aspx");
            routeCollection.MapPageRoute("Kandidat-Kategorija", "kandidat/prijavljena-kategorija", "~/forms/kandidat/novaKategorija.aspx");
            routeCollection.MapPageRoute("Kandidat-Ucenje", "kandidat/ucenje", "~/forms/kandidat/ucenje.aspx");
            routeCollection.MapPageRoute("Kandidat-Provjera", "kandidat/provjera-znanja", "~/forms/kandidat/provjera.aspx");
            routeCollection.MapPageRoute("Kandidat-404", "kandidat/404", "~/forms/kandidat/404.aspx");
            routeCollection.MapPageRoute("Kandidat-Instruktor", "kandidat/instruktor", "~/forms/kandidat/pregledInstruktor.aspx");
            routeCollection.MapPageRoute("Kandidat-Profil", "kandidat/profil", "~/forms/kandidat/profil.aspx");
            routeCollection.MapPageRoute("Kandidat-Rezultat", "kandidat/provjera-znanja/rezultat", "~/forms/kandidat/provjeraRezultat.aspx");
            routeCollection.MapPageRoute("Kandidat-UradjenaPriprema", "kandidat/priprema", "~/forms/kandidat/priprema.aspx");

            /*------------Instruktor-------------*/
            routeCollection.MapPageRoute("Instruktor-NovaVrstaPitanja", "instruktor/novavrstapitanja", "~/forms/instruktor/novaDodajVrstaPitanja.aspx");
            routeCollection.MapPageRoute("Instruktor-NovaKategorija", "instruktor/novakategorija", "~/forms/instruktor/novaKategorija.aspx");            
            routeCollection.MapPageRoute("Instruktor-Kategorija", "instruktor/kategorija", "~/forms/instruktor/kategorija.aspx");
            routeCollection.MapPageRoute("Instruktor-Naslovnica", "instruktor/naslovnica", "~/forms/instruktor/index.aspx");
            routeCollection.MapPageRoute("Instruktor-VrstaPitanja", "instruktor/vrstapitanja", "~/forms/instruktor/VrstaPitanja.aspx");
            routeCollection.MapPageRoute("Instruktor-NovoPitanje", "instruktor/novopitanje", "~/forms/instruktor/DodajPitanje.aspx");
            routeCollection.MapPageRoute("Instruktor-NovaPrijava", "instruktor/novaprijava", "~/forms/instruktor/dodajPrijavu.aspx");
            routeCollection.MapPageRoute("Instruktor-Prijave", "instruktor/prijave", "~/forms/instruktor/novaPrijave.aspx");
            routeCollection.MapPageRoute("Instruktor-Prijava", "instruktor/prijava", "~/forms/instruktor/pregledPrijave.aspx");
            routeCollection.MapPageRoute("Instruktor-NoviKandidat", "instruktor/novikandidat", "~/forms/instruktor/novaDodajKandidat.aspx");
            routeCollection.MapPageRoute("Instruktor-Kandidati", "instruktor/kandidati", "~/forms/instruktor/allKandidati.aspx");
            routeCollection.MapPageRoute("Instruktor-Kandidat", "instruktor/kandidat", "~/forms/instruktor/novaKandidat.aspx");
            routeCollection.MapPageRoute("Instruktor-Profil", "instruktor/profil", "~/forms/instruktor/profil.aspx");
            routeCollection.MapPageRoute("Instruktor-404", "instruktor/404", "~/forms/instruktor/404.aspx");
            routeCollection.MapPageRoute("Instruktor-IzmjenaPitanja", "instruktor/izmjena-pitanja", "~/forms/instruktor/novaIzmjenaPitanja.aspx");
           
        }


        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Response.Redirect("/prijava");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}