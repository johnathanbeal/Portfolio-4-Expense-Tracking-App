using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using YNAET.Models;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YNAET.Controllers
{
    [Produces("application/json")]
    public class ExpensesController : Controller
    {
        private readonly INHibernateSession _nHibernateSession;

        public ExpensesController(INHibernateSession nHibernateSession)
        {
            _nHibernateSession = nHibernateSession;
        }
        //ViewBag Message goes here

        public ActionResult Index()
        {
            IList<Expense> expenses;

            using (ISession session = _nHibernateSession.OpenSession())
            {
                expenses = session.Query<Expense>().ToList();
            }
            return View(expenses);
        }

        // GET: Expense/Details/5
        public ActionResult Details(int id)
        {
            Expense expense = new Expense();
            using (ISession session = _nHibernateSession.OpenSession())
            {
                expense = session.Query<Expense>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(expense);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Expense expense = new Expense();
                expense.account = collection["title"].ToString();
                expense.amount = Convert.ToInt32(collection["amount"]);
                expense.category = collection["category"].ToString();
                expense.colorCode = collection["colorCode"].ToString();
                expense.date = DateTime.Parse(collection["date"]);
                //expense.id = Convert.ToInt32(collection["id"]);
                expense.impulse = bool.Parse(collection["impulse"]);
                expense.memo = collection["memo"].ToString();
                expense.payee = collection["payee"].ToString();
                expense.repeat = bool.Parse(collection["repeat"]);

                using (ISession session = _nHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(expense);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
            
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            Expense expense = new Expense();
            using (ISession session = _nHibernateSession.OpenSession())
            {
                expense = session.Query<Expense>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(expense);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Expense expense = new Expense();
                expense.account = collection["title"].ToString();
                expense.amount = Convert.ToInt32(collection["amount"]);
                expense.category = collection["category"].ToString();
                expense.colorCode = collection["colorCode"].ToString();
                expense.date = DateTime.Parse(collection["date"]);
                //expense.id = Convert.ToInt32(collection["id"]);
                expense.impulse = bool.Parse(collection["impulse"]);
                expense.memo = collection["memo"].ToString();
                expense.payee = collection["payee"].ToString();
                expense.repeat = bool.Parse(collection["repeat"]);


                // TODO: Add insert logic here
                using (ISession session = _nHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(expense);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            // Delete the book
            Expense expense = new Expense();
            using (ISession session = _nHibernateSession.OpenSession())
            {
                expense = session.Query<Expense>().Where(b => b.id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", expense);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (ISession session = _nHibernateSession.OpenSession())
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
