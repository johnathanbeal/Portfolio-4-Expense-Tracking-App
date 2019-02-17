using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using YNAET.Controllers;
using YNAET.Models;
using YNAET.Nibernate;

namespace YNAET.Tests
{
    [TestFixture]
    public class GetControllerShould
    {
        [Test]
        public void FirstUnitTest()
        {
            var expenseMock = new Mock<ExpenseInputModel>();
            var sessionMock = new Mock<INHibernateSession>();
            var mockExpenseGetControler = new ExpensesGetController(sessionMock.Object);

            var result = mockExpenseGetControler.Details(1);
            JsonResult expectedType = new JsonResult(expenseMock);
            Assert.IsInstanceOf(expectedType.GetType(), result);
        }
    }
}
