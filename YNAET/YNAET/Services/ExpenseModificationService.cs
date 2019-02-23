using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YNAET.Nibernate;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using YNAET.Models;
using YNAET.Entities;
using ISession = NHibernate.ISession;

namespace YNAET.Services
{
    public class ExpenseModificationService
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpenseModificationService(INHibernateSession nHibernateSession)
        {
            _inHibernateSession = nHibernateSession;
        }


        [HttpPut("api/expenses/{id}")]
        public ActionResult Modify(int id, [FromBody]ExpenseInputModel expenseInputModel)
        {
            using (ISession session = _inHibernateSession.OpenSession())
            {
                var expenseEntity = session.Query<ExpenseEntity>().Where(b => b.Id == id).FirstOrDefault();
                expenseEntity.Account = expenseInputModel.Account;
                expenseEntity.Category = expenseInputModel.Category;
                expenseEntity.ColorCode = expenseInputModel.ColorCode;
                expenseEntity.Memo = expenseInputModel.Memo;
                expenseEntity.Payee = expenseInputModel.Payee;
                expenseEntity.Amount = expenseInputModel.Amount;
                expenseEntity.Date = expenseInputModel.Date;
                expenseEntity.Impulse = expenseInputModel.Impulse;
                expenseEntity.Repeat = expenseInputModel.Repeat;
                expenseEntity.Repeat = expenseEntity.Repeat;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(expenseEntity);
                    transaction.Commit();
                }
            }

            return new OkResult();
        }
    }
}
