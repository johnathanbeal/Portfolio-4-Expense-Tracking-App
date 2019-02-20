using Microsoft.AspNetCore.Mvc;
using Moq;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YNAET.Entities;
using YNAET.Models;
using YNAET.Nibernate;
using YNAET.Services;

namespace YNAET.Tests
{
    [TestFixture]
    public class PostControllerTest
    {
        [Test]
        public void Added_Expense_Should_Save()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 10,
                Payee = "Amazon",
                Amount = 18.00M,
                Category = "Stuff I Forget to Budget For",
                Account = "Middleburg",
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Dry Erase Paper Sheets",
                ColorCode = "Blue"
            };

            var transaction = new Mock<ITransaction>();
            var session = new Mock<ISession>();
            session.Setup(t => t.BeginTransaction())
                .Returns(transaction.Object);

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            session.Setup(x => x.Save(expenseInput))
                .Returns(() => expenseInput);

            var expenseCreationService = new ExpenseCreationService(nhibernateSession.Object);
            var post = expenseCreationService.Post(expenseInput);

            session.Verify(x => x.Save(expenseInput));
        }

        [Test]
        public void Added_Expense_Should_Commit()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 10,
                Payee = "Amazon",
                Amount = 18.00M,
                Category = "Stuff I Forget to Budget For",
                Account = "Middleburg",
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Dry Erase Paper Sheets",
                ColorCode = "Blue"

            };

            var transaction = new Mock<ITransaction>();
            var session = new Mock<ISession>();
            session.Setup(t => t.BeginTransaction())
                .Returns(transaction.Object);

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            

            var expenseCreationService = new ExpenseCreationService(nhibernateSession.Object);
            var post = expenseCreationService.Post(expenseInput);

            transaction.Verify(x => x.Commit(), Times.Once);

        }

        [Test]
        public void Should_Added_Expense_Should_Not_Return_Null()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 10,
                Payee = "Amazon",
                Amount = 18.00M,
                Category = "Stuff I Forget to Budget For",
                Account = "Middleburg",
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Dry Erase Paper Sheets",
                ColorCode = "Blue"

            };

            var transaction = new Mock<ITransaction>();
            var session = new Mock<ISession>();
            session.Setup(t => t.BeginTransaction())
                .Returns(transaction.Object);



            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var expenseCreationService = new ExpenseCreationService(nhibernateSession.Object);
            var post = expenseCreationService.Post(expenseInput);

            Assert.IsNotNull(post);

            
        }

        [Test]
        public void Should_Added_Expense_Should_Return_Json()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 10,
                Payee = "Amazon",
                Amount = 18.00M,
                Category = "Stuff I Forget to Budget For",
                Account = "Middleburg",
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Dry Erase Paper Sheets",
                ColorCode = "Blue"

            };

            var transaction = new Mock<ITransaction>();
            var session = new Mock<ISession>();
            session.Setup(t => t.BeginTransaction())
                .Returns(transaction.Object);



            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var expenseCreationService = new ExpenseCreationService(nhibernateSession.Object);
            var post = expenseCreationService.Post(expenseInput);

            var expectedType = new JsonResult(expenseInput);
            var result = post as JsonResult;

            Assert.IsInstanceOf(expectedType.GetType(), result);
        }
    }
}
