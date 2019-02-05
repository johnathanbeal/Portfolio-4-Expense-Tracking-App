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
            IList<ExpenseModel> expenses;

            using (ISession session = _inHibernateSession.OpenSession())
            {
                expenses = session.Query<ExpenseModel>().ToList();
            }
            return new JsonResult(expenses);
        }

        [HttpGet("api/expenses/{id}")]
        public ActionResult Details(int id)
        {
            ExpenseModel expense = new ExpenseModel();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<ExpenseModel>().Where(b => b.Id == id).FirstOrDefault();
            }

            return new JsonResult(expense);
        }
        
    }
}
