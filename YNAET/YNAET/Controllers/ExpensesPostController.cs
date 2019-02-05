using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;
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
                var expenseModel = new ExpenseModel();
                expenseModel.Account = expenseInputModel.Account;
                expenseModel.Amount = expenseInputModel.Amount;
                expenseModel.Category = expenseInputModel.Category;
                expenseModel.ColorCode = expenseInputModel.ColorCode;
                expenseModel.Date = expenseInputModel.Date;
                expenseModel.Impulse = expenseInputModel.Impulse;
                expenseModel.Memo = expenseInputModel.Memo;
                expenseModel.Payee = expenseInputModel.Payee;
                expenseModel.Repeat = expenseInputModel.Repeat;

                using (ISession session = _inHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(expenseModel);
                        transaction.Commit();
                    }
                }
                return new JsonResult(expenseModel);
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }

        }
    }
}
