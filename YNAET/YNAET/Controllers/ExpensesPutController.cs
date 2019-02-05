using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Linq;
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

        [HttpPut("api/expenses/edit/{id}")]
        public ActionResult Edit(int id, [FromBody]ExpenseInputModel expenseInputModel)
        {
            ExpenseModel expenseModel = new ExpenseModel();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expenseModel = session.Query<ExpenseModel>().Where(b => b.Id == id).FirstOrDefault();
                expenseModel.Account = expenseInputModel.Account ?? expenseModel.Account;
                expenseModel.Category = expenseInputModel.Category ?? expenseModel.Category;
                expenseModel.ColorCode = expenseInputModel.ColorCode ?? expenseModel.ColorCode;
                expenseModel.Memo = expenseInputModel.Memo ?? expenseModel.Memo;
                expenseModel.Payee = expenseInputModel.Payee ?? expenseModel.Payee;

                if (expenseInputModel.Amount == 0)
                {
                    expenseModel.Amount = expenseModel.Amount;
                }
                else
                {
                    expenseModel.Amount = expenseInputModel.Amount;
                }

                if (expenseInputModel.Date == DateTime.MinValue)
                {
                    expenseModel.Date = expenseModel.Date;
                }
                else
                {
                    expenseModel.Date = expenseInputModel.Date;
                }

                try
                { 
                    expenseModel.Impulse = expenseInputModel.Impulse;
                }
                catch
                {
                    expenseModel.Impulse = expenseModel.Impulse;
                }

                try
                {
                    expenseModel.Repeat = expenseInputModel.Repeat;
                }
                catch
                {
                    expenseModel.Repeat = expenseModel.Repeat;
                }
               

                using (ITransaction transaction = session.BeginTransaction())//DO I NEED THIS?
                {
                    session.SaveOrUpdate(expenseModel);//DO I NEED THIS?
                    transaction.Commit();//DO I NEED THIS?
                }
            }

            ViewBag.SubmitAction = "Save";// DO I NEED THIS?
            return new JsonResult(expenseModel);
        }
    }
}
