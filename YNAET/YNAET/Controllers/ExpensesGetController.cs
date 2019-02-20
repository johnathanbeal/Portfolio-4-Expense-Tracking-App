using Microsoft.AspNetCore.Mvc;
using YNAET.Services;

namespace YNAET.Controllers
{
    public class ExpensesGetController : Controller
    {
        private readonly IExpenseQueryService _iExpenseQueryService;

        public ExpensesGetController(IExpenseQueryService iExpenseQueryService)
        {
            _iExpenseQueryService = iExpenseQueryService;
        }
       
        [HttpGet("api/expenses")]
        public IActionResult Execute()
        {
            var expenses = _iExpenseQueryService.QueryAll();
            return new JsonResult(expenses);
        }

        [HttpGet("api/expenses/{id}")]
        public IActionResult Details(int id)
        {
            var expense = _iExpenseQueryService.Query(id);
            return new JsonResult(expense);

        }
        
    }
}
