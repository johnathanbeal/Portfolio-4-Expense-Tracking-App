using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;


namespace YNAET.Services
{
    public interface IExpenseQueryService
    {
        IActionResult Query(int id);
        IActionResult QueryAll();
    }
    public class ExpenseQueryService : IExpenseQueryService
    {
        private INHibernateSession _inHibernateSession;
        private ExpenseEntity _expense;

        public ExpenseQueryService(INHibernateSession nHibernateSession)
        {
            _inHibernateSession = nHibernateSession;
        }

        public IActionResult QueryAll()
        {
            IList<ExpenseEntity> expenses;

            using (ISession session = _inHibernateSession.OpenSession())
            {
                expenses = session.Query<ExpenseEntity>().ToList();
            }
            return new JsonResult(expenses);
        }

        public IActionResult Query(int id)
        {
            using (ISession session = _inHibernateSession.OpenSession())
            {
                _expense = session.Query<ExpenseEntity>().Where(b => b.Id == id).FirstOrDefault();
            }

            return new JsonResult(_expense);
        }
    }
}
