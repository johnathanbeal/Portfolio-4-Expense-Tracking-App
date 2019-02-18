using Microsoft.AspNetCore.Mvc;
using Moq;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YNAET.Controllers;
using YNAET.Entities;
using YNAET.Models;
using YNAET.Nibernate;

namespace YNAET.Tests
{
    [TestFixture]
    public class GetControllerTest
    {
       
        [Test]
        public void Should_Get_Return_Type_Of_JsonResult()
        {
            var expenseMock = new Mock<ExpenseInputModel>();

            var expenseEntity = new ExpenseEntity()
            {
                Id = 1,
                Payee = "Toys R Us",
                Amount = 100.00M,
                Category = "Fun Money",
                Account = "Suntrust",
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "He-man Toys",
                ColorCode = "Pink"

            };

            var session = new Mock<ISession>();
            session.Setup(x => x.Query<ExpenseEntity>())
                .Returns(() => new List<ExpenseEntity>().AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            var result = mockExpenseGetController.Details(1);
            Assert.IsNotNull(result);
            JsonResult expectedType = new JsonResult(expenseEntity);
            Assert.IsInstanceOf(expectedType.GetType(), result);

        }
    }
}
