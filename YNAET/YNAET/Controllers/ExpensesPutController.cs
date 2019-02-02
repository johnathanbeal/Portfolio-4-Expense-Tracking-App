using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;

namespace YNAET.Controllers
{

    public class ExpensesPutController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        private ExpensesPutController(INHibernateSession inHibernateSession)
        {
            _inHibernateSession = inHibernateSession;
        }

        [HttpPut("api/expenses/edit/{id}")]
        public ActionResult Edit(int id)
        {
            Expense expense = new Expense();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<Expense>().Where(b => b.id == id).FirstOrDefault();
            }

            ViewBag.SubmitAction = "Save";
            return View(expense);
        }

        [HttpPut("api/expenses/edit/{id}/{ihavenoideawhatiamdoing}")]
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

                using (ISession session = _inHibernateSession.OpenSession())
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
    }
}
