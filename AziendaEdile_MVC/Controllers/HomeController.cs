using AziendaEdile_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AziendaEdile_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString;
        public static List<Dipendente> listadipendenti = new List<Dipendente>();
        public static List<Pagamento> listaPagamenti = new List<Pagamento>();

        public HomeController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["AziendaEdileDB"].ConnectionString;
        }

        // GET: Home
        public ActionResult Index()
        {
            listadipendenti.Clear();
            listaPagamenti.Clear();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand queryVisualizzaDip = new SqlCommand("SELECT * FROM Dipendenti", conn);
                SqlDataReader readerDipendenti = queryVisualizzaDip.ExecuteReader();
                while (readerDipendenti.Read())
                {
                    Dipendente dipendente = new Dipendente()
                    {
                        ID = (int)readerDipendenti["ID"],
                        Nome = readerDipendenti["Nome"].ToString(),
                        Cognome = readerDipendenti["Cognome"].ToString(),
                        Indirizzo = readerDipendenti["Indirizzo"].ToString(),
                        CodiceFiscale = readerDipendenti["CodiceFiscale"].ToString(),
                        Coniugato = (bool)readerDipendenti["Coniugato"],
                        NumeroFigli = (int)readerDipendenti["NumeroFigli"],
                        Mansione = readerDipendenti["Mansione"].ToString()
                    };

                    listadipendenti.Add(dipendente);
                }
                readerDipendenti.Close();

                SqlCommand queryVisualizzaPagamenti = new SqlCommand("SELECT * FROM Pagamenti", conn);
                SqlDataReader readerPagamenti = queryVisualizzaPagamenti.ExecuteReader();
                while (readerPagamenti.Read())
                {
                    Pagamento pagamento = new Pagamento()
                    {
                        IdPagamento = (int)readerPagamenti["IdPagamento"],
                        IDDipendente = (int)readerPagamenti["IDDipendente"],
                        PeriodoPagamento = readerPagamenti["PeriodoPagamento"].ToString(),
                        Ammontare = (decimal)readerPagamenti["Ammontare"],
                        Tipo = readerPagamenti["Tipo"].ToString()
                    };

                    listaPagamenti.Add(pagamento);
                }
                readerPagamenti.Close();

                return View(listadipendenti);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return View(listadipendenti);
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
        return View();
    }

        [HttpPost]
        public ActionResult CreateEmployee(Dipendente dipendente)
        {
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    SqlCommand queryDipenednte = new SqlCommand("INSERT INTO Dipendenti (Nome, Cognome, Indirizzo, CodiceFiscale, Coniugato, NumeroFigli, Mansione) VALUES (@Nome, @Cognome, @Indirizzo, @CodiceFiscale, @Coniugato, @NumeroFigli, @Mansione)", conn);

                    queryDipenednte.Parameters.AddWithValue("@Nome", dipendente.Nome);
                    queryDipenednte.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                    queryDipenednte.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                    queryDipenednte.Parameters.AddWithValue("@CodiceFiscale", dipendente.CodiceFiscale);
                    queryDipenednte.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                    queryDipenednte.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                    queryDipenednte.Parameters.AddWithValue("@Mansione", dipendente.Mansione);

                    int inserimentoeffettuato = queryDipenednte.ExecuteNonQuery();

                    if (inserimentoeffettuato > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch(Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
          
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            Dipendente dipendente = new Dipendente();

            try
            {
                conn.Open();
                SqlCommand queryVisualizzaDip = new SqlCommand("SELECT * FROM Dipendenti WHERE ID = @ID", conn);
                queryVisualizzaDip.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = queryVisualizzaDip.ExecuteReader();

                if (reader.Read())
                {
                    dipendente.ID = (int)reader["ID"];
                    dipendente.Nome = reader["Nome"].ToString();
                    dipendente.Cognome = reader["Cognome"].ToString();
                    dipendente.Indirizzo = reader["Indirizzo"].ToString();
                    dipendente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    dipendente.Coniugato = (bool)reader["Coniugato"];
                    dipendente.NumeroFigli = (int)reader["NumeroFigli"];
                    dipendente.Mansione = reader["Mansione"].ToString();
                }
                else
                {

                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return View(dipendente);
        }

        [HttpPost]
        public ActionResult EditEmployee(Dipendente dipendente)
        {
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    SqlCommand queryAggiornaDip = new SqlCommand("UPDATE Dipendenti SET Nome = @Nome, Cognome = @Cognome, Indirizzo = @Indirizzo, CodiceFiscale = @CodiceFiscale, Coniugato = @Coniugato, NumeroFigli = @NumeroFigli, Mansione = @Mansione WHERE ID = @ID", conn);

                    queryAggiornaDip.Parameters.AddWithValue("@ID", dipendente.ID);
                    queryAggiornaDip.Parameters.AddWithValue("@Nome", dipendente.Nome);
                    queryAggiornaDip.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                    queryAggiornaDip.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                    queryAggiornaDip.Parameters.AddWithValue("@CodiceFiscale", dipendente.CodiceFiscale);
                    queryAggiornaDip.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                    queryAggiornaDip.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                    queryAggiornaDip.Parameters.AddWithValue("@Mansione", dipendente.Mansione);

                    int aggiornamentoEffettuato = queryAggiornaDip.ExecuteNonQuery();

                    if (aggiornamentoEffettuato > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
 
            return View(dipendente);
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand queryEliminaDip = new SqlCommand("DELETE FROM Dipendenti WHERE ID = @ID", conn);
                queryEliminaDip.Parameters.AddWithValue("@ID", id);

                int eliminazioneEffettuata = queryEliminaDip.ExecuteNonQuery();

                if (eliminazioneEffettuata > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return HttpNotFound();
        }

    }
}