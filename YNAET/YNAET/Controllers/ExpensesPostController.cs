using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;
using YNAET.Models;
using YNAET.Entities;
using YNAET.Services;

namespace YNAET.Controllers
{
    public class ExpensesPostController : Controller
    {
        private readonly IExpenseCreationService _iExpenseCreationService;


        public ExpensesPostController(IExpenseCreationService iExpenseCreationService)
        {
            _iExpenseCreationService = iExpenseCreationService;
        }
        
        [HttpPost("api/expenses")]
        public IActionResult Post([FromBody]ExpenseInputModel ExpenseInputModel) 
        {
            var expense = _iExpenseCreationService.Create(ExpenseInputModel);
            return expense;

        }
    }
}
