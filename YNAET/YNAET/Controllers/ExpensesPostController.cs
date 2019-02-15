using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;
using YNAET.Models;
using YNAET.Entities;

namespace YNAET.Controllers
{
    public class ExpensesPostController : Controller
    {
        private readonly INHibernateSession _inHibernateSession;

        public ExpensesPostController(INHibernateSession inHibernateSession)
        {
            _inHibernateSession = inHibernateSession;
        }
        
        [HttpPost("api/expenses")]
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
