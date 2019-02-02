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
    public class ExpensesPostController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpensesPostController(INHibernateSession inHibernateSession)
        {
            _inHibernateSession = inHibernateSession;
        }

        [HttpPost("api/expenses/create")]
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

                using (ISession session = _inHibernateSession.OpenSession())
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
    }
}
