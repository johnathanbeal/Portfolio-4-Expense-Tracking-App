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
        private readonly ExpenseCreationService _expenseCreationService;


        public ExpensesPostController(IExpenseCreationService iExpenseCreationService)
        {
            _iExpenseCreationService = iExpenseCreationService;
        }
        
        [HttpPost("api/expenses")]
        public ActionResult Post([FromBody]ExpenseInputModel expenseInputModels) 
        {
            return _iExpenseCreationService.Post(expenseInputModels);

        }
    }
}
