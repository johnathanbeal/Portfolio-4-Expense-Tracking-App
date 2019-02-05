using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YNAET.Entities;
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
            ExpenseEntity expense = new ExpenseEntity();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<ExpenseEntity>().Where(b => b.Id == id).FirstOrDefault();
            }
            ViewBag.SubmitAction = "Confirm delete";
            return new JsonResult(expense);
        }
    }
}
