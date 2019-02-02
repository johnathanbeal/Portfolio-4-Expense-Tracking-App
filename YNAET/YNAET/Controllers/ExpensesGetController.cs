using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;

namespace YNAET.Controllers
{
    public class ExpensesGetController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpensesGetController(INHibernateSession nHibernateSession)
        {
            _inHibernateSession = nHibernateSession;
        }

        [HttpGet("api/expenses")]
        public IActionResult Execute()
        {
            IList<Expense> expenses;

            using (ISession session = _inHibernateSession.OpenSession())
            {
                expenses = session.Query<Expense>().ToList();
            }
            return new JsonResult(expenses);
        }

        [HttpGet("api/expenses/{id}")]
        public ActionResult Details(int id)
        {
            Expense expense = new Expense();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<Expense>().Where(b => b.id == id).FirstOrDefault();
            }

            return View(expense);
        }
        
    }
}
