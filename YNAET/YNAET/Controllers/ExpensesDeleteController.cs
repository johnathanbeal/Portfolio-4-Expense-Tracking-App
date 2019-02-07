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
            if (id == null)
            {
                return NotFound();
            }

            // Delete the book
            ExpenseEntity expense = new ExpenseEntity();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<ExpenseEntity>().Where(b => b.Id == id).FirstOrDefault();
                if (expense == null)
                {
                    return NotFound();
                }
                else
                {
                    session.Delete(expense)
                  
                }
            }

            return new OkResult();
        }
    }
}
