using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;
using YNAET.Services;

namespace YNAET.Controllers
{
    public class ExpensesGetController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpensesGetController()
        {
        }
       
        [HttpGet("api/expenses")]
        public IActionResult Execute()
        {
            IList<ExpenseEntity> expenses;

            using (ISession session = _inHibernateSession.OpenSession())
            {
                expenses = session.Query<ExpenseEntity>().ToList();
            }
            return new JsonResult(expenses);
        }

        [HttpGet("api/expenses/{id}")]
        public ExpenseEntity Details(int id)
        {
            IExpenseQueryService expenseData = new ExpenseQueryService(_inHibernateSession);
            return expenseData.Query(id);

        }
        
    }
}
