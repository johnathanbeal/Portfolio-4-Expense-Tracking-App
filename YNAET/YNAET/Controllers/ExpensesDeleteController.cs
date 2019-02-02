using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YNAET.Models;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;

namespace YNAET.Controllers
{
    public class ExpensesDeleteController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpensesDeleteController(INHibernateSession iNHibernateSession)
        {
            _inHibernateSession = iNHibernateSession;
        }

        [HttpDelete("api/expenses/{id}")]
        public ActionResult Delete(int id)
        {
            // Delete the book
            Expense expense = new Expense();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<Expense>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", expense);
        }

        // POST: Book/Delete/5
        [HttpDelete("api/expenses/{id}/{ihavenoideawhatiamdoing}")]
        public ActionResult Delete(long id, FormCollection collection)
        {
            try
            {
                using (ISession session = _inHibernateSession.OpenSession())
                {
                    Expense expense = session.Get<Expense>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(expense);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
