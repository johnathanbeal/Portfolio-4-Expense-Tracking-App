﻿using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;


namespace YNAET.Services
{
    public interface IExpenseQueryService
    {
        
        List<ExpenseEntity> QueryAll();
        ExpenseEntity Query(int id);
    }
    public class ExpenseQueryService : IExpenseQueryService
    {
        private INHibernateSession _inHibernateSession;

        public ExpenseQueryService(INHibernateSession nHibernateSession)
        {
            _inHibernateSession = nHibernateSession;
        }

        public List<ExpenseEntity> QueryAll()
        {
            List<ExpenseEntity> expenses;

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
                var expense = session.Query<ExpenseEntity>().Where(b => b.Id == id).FirstOrDefault();

                return expense;
            }
        }
    }
}
