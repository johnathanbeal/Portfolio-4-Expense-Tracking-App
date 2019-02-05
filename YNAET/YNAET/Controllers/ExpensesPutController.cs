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
        public ActionResult Edit(int id)
        {
            ExpenseModel expense = new ExpenseModel();
            using (ISession session = _inHibernateSession.OpenSession())
            {
                expense = session.Query<ExpenseModel>().Where(b => b.Id == id).FirstOrDefault();
                using (ITransaction transaction = session.BeginTransaction())//DO I NEED THIS?
                {
                    session.SaveOrUpdate(expense);//DO I NEED THIS?
                    transaction.Commit();//DO I NEED THIS?
                }
            }

            ViewBag.SubmitAction = "Save";// DO I NEED THIS?
            return new JsonResult(expense);
        }
    }
}
