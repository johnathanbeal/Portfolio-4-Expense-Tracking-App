using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Linq;
using YNAET.Models;
using YNAET.Entities;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;
using YNAET.Services;

namespace YNAET.Controllers
{

    public class ExpensesPutController : Controller
    {
        private readonly ExpenseModificationService _expenseModificationService;

        public ExpensesPutController(ExpenseModificationService expenseModificationService)
        {
            _expenseModificationService = expenseModificationService;
        }


        [HttpPut("api/expenses/{id}")]
        public ActionResult Edit(int id, [FromBody]ExpenseInputModel expenseInputModel)
        {
            var expense = _expenseModificationService.Modify(id, expenseInputModel);
            return new JsonResult(expense);
        }
    }
}
