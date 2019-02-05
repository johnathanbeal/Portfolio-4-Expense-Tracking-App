using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Linq;
using YNAET.Models;
using YNAET.Entities;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;

namespace YNAET.Controllers
{

    public class ExpensesPutController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpensesPutController(INHibernateSession inHibernateSession)
        {
            _inHibernateSession = inHibernateSession;
        }

        [HttpPut("api/expenses/{id}")]
        public ActionResult Edit(int id, [FromBody]ExpenseInputModel expenseInputModel)
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
