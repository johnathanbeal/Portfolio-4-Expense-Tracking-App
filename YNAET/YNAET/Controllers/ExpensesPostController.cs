using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;
using YNAET.Entities;
using System.Threading.Tasks;

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

        [HttpPost("api/expense")]
        public ActionResult Post([FromBody]Expense expenseModel) 
        {
            var addExpense2 = expenseModel;

            try
            {
                Expense expenseDB = new Expense();
                expenseDB.account = expenseModel.account;
                expenseDB.amount = expenseModel.amount;
                expenseDB.category = expenseModel.category;
                expenseDB.colorCode = expenseModel.colorCode;
                expenseDB.date = expenseModel.date;
                expenseDB.impulse = expenseModel.impulse;
                expenseDB.memo = expenseModel.memo;
                expenseDB.payee = expenseModel.payee;
                expenseDB.repeat = expenseModel.repeat;

                using (ISession session = _inHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(expenseModel);
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
