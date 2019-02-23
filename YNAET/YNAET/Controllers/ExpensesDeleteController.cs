using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;
using YNAET.Services;
using ISession = NHibernate.ISession;

namespace YNAET.Controllers
{
    public class ExpensesDeleteController : Controller
    {
        private readonly IExpenseDeletionService _iExpenseDeletionService;

        public ExpensesDeleteController(IExpenseDeletionService iExpenseDeletionService)
        {
            _iExpenseDeletionService = iExpenseDeletionService;
        }

        [HttpDelete("api/expenses/{id}")]
        public IActionResult Delete(int id)
        {
            //ExpenseEntity expense = new ExpenseEntity();
            //using (ISession session = _inHibernateSession.OpenSession())
            //{
            //    expense = session.Query<ExpenseEntity>().
            //        Where(b => b.Id == id).FirstOrDefault();
            //    if (expense == null)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        using (ITransaction transaction = session.BeginTransaction())
            //        {
            //            session.Delete(expense);
            //            transaction.Commit();
            //        }                      
            //    }
            //}
            //return new OkResult();
            return _iExpenseDeletionService.Drop(id);

        }
    }
}
