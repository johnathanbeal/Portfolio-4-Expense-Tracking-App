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
        ExpenseEntity Query(int id);
    }
    public class ExpenseQueryService : IExpenseQueryService
    {
        private readonly INHibernateSession _inHibernateSession;
        private ExpenseEntity _expense;

        public ExpenseQueryService(INHibernateSession nHibernateSession)
        {
            nHibernateSession = _inHibernateSession;
        }

        public IList<ExpenseEntity> QueryAll()
        {
            IList<ExpenseEntity> expenses;

            using (ISession session = _inHibernateSession.OpenSession())
            {
                expenses = session.Query<ExpenseEntity>().ToList();
            }
            return expenses;
        }

        public ExpenseEntity Query(int id)
        {
            using (ISession session = _inHibernateSession.OpenSession())
            {
                _expense = session.Query<ExpenseEntity>().Where(b => b.Id == id).FirstOrDefault();
            }

            return _expense;
        }
    }
}
