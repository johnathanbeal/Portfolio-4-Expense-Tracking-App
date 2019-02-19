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
        private readonly IExpenseQueryService _iExpenseQueryService;

        public ExpensesGetController(IExpenseQueryService iExpenseQueryService)
        {
            _iExpenseQueryService = iExpenseQueryService;
        }
       
        [HttpGet("api/expenses")]
        public IActionResult Execute()
        {
            //IList<ExpenseEntity> expenses;

            //using (ISession session = _inHibernateSession.OpenSession())
            //{
            //    expenses = session.Query<ExpenseEntity>().ToList();
            //}
            return _iExpenseQueryService.QueryAll();
        }

        [HttpGet("api/expenses/{id}")]
        public IActionResult Details(int id)
        {
            return _iExpenseQueryService.Query(id);

        }
        
    }
}
