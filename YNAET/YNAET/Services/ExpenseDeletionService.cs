﻿using NHibernate;
using System;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;
using YNAET.Exceptions;

namespace YNAET.Services
{
    public interface IExpenseDeletionService
    {
        void Drop(int it);
    }

    public class ExpenseDeletionService : IExpenseDeletionService
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpenseDeletionService(INHibernateSession iNHibernateSession)
        {
            _inHibernateSession = iNHibernateSession;
        }

        public void Drop(int id)
        {
            using (ISession session = _inHibernateSession.OpenSession())
            {
                var expense = session.Query<ExpenseEntity>().
                    Where(b => b.Id == id).FirstOrDefault();
                if (expense == null)
                {
                    throw new NotFoundException();
                }
                else
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(expense);
                        transaction.Commit();
                    }
                }
            }
        }
    }

    
}
