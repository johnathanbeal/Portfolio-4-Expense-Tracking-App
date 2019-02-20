using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using YNAET.Entities;
using YNAET.Models;
using YNAET.Nibernate;

namespace YNAET.Services
{
    
    public interface IExpenseCreationService
    {
        ActionResult Post([FromBody]ExpenseInputModel expenseInputModels);
    }

    public class ExpenseCreationService : IExpenseCreationService
    {
        private INHibernateSession _inHibernateSession;

        public ExpenseCreationService(INHibernateSession inHibernateSession)
        {
            _inHibernateSession = inHibernateSession;
        }

        public ActionResult Post([FromBody]ExpenseInputModel expenseInputModels)
        {
            try
            {
                var expenseEntity = new ExpenseEntity();
                expenseEntity.Account = expenseInputModels.Account;
                expenseEntity.Amount = expenseInputModels.Amount;
                expenseEntity.Category = expenseInputModels.Category;
                expenseEntity.ColorCode = expenseInputModels.ColorCode;
                expenseEntity.Date = expenseInputModels.Date;
                expenseEntity.Impulse = expenseInputModels.Impulse;
                expenseEntity.Memo = expenseInputModels.Memo;
                expenseEntity.Payee = expenseInputModels.Payee;
                expenseEntity.Repeat = expenseInputModels.Repeat;

                using (ISession session = _inHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(expenseEntity);
                        transaction.Commit();
                    }
                }
                return new JsonResult(expenseEntity);
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }
    }

    
}
