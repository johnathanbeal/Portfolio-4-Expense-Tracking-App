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
        public ActionResult Post([FromBody]ExpenseInputModel expenseInputModel) 
        {
            try
            {
                var expenseEntity = new ExpenseEntity();
                expenseEntity.Account = expenseInputModel.Account;
                expenseEntity.Amount = expenseInputModel.Amount;
                expenseEntity.Category = expenseInputModel.Category;
                expenseEntity.ColorCode = expenseInputModel.ColorCode;
                expenseEntity.Date = expenseInputModel.Date;
                expenseEntity.Impulse = expenseInputModel.Impulse;
                expenseEntity.Memo = expenseInputModel.Memo;
                expenseEntity.Payee = expenseInputModel.Payee;
                expenseEntity.Repeat = expenseInputModel.Repeat;

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
