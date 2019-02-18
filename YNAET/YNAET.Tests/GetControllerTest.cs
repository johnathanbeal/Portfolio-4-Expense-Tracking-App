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
                .Returns(() => new List<ExpenseEntity>{ expenseEntity }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            JsonResult expectedType = new JsonResult(expenseEntity);
            var result = mockExpenseGetController.Details(1);
            Assert.IsInstanceOf(expectedType.GetType(), result);

        }

        [Test]
        public void Should_Not_Have_Null_Type()
        {
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
                .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            var result = mockExpenseGetController.Details(1);
            Assert.IsNotNull(result);

        }

        [Test]
        public void Should_Not_Have_Null_Value()
        {
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
                .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            var result = mockExpenseGetController.Details(1);
            Assert.IsNotNull(result.Value);

        }


        [Test]
        public void Should_Equal_Expense()
        {
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
                .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            var result = mockExpenseGetController.Details(1);
            Assert.AreEqual(result.Value, expenseEntity);
        }

        [Test]
        public void Should_Be_Same_Expense()
        {
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
                .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            var result = mockExpenseGetController.Details(1);
            Assert.AreSame(result.Value, expenseEntity);
        }

        [Test]
        public void Should_Have_Equal_Input_Output_Expense()
        {
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
                .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var mockExpenseGetController = new ExpensesGetController(nhibernateSession.Object);

            var result = mockExpenseGetController.Details(1).Value as ExpenseEntity;
            Assert.AreEqual(result, expenseEntity);
        }
    }
}
