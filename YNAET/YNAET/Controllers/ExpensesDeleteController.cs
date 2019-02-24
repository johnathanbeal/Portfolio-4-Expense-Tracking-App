using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;
using YNAET.Services;
using ISession = NHibernate.ISession;

namespace YNAET.Controllers
{
    public class ExpensesDeleteController : Controller
    {
        private readonly IExpenseDeletionService _iExpenseDeletionService;

        public ExpensesDeleteController(IExpenseDeletionService iExpenseDeletionService)
        {
            _iExpenseDeletionService = iExpenseDeletionService;
        }

        [HttpDelete("api/expenses/{id}")]
        public IActionResult Delete(int id)
        {
            _iExpenseDeletionService.Drop(id);
            return new OkResult();

        }
    }
}
