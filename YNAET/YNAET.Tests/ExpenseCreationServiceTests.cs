using System;
using Moq;
using NHibernate;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using YNAET.Entities;
using YNAET.Models;
using YNAET.Nibernate;
using YNAET.Services;

namespace YNAET.Tests
{
    [TestFixture]
    public class ExpenseCreationServiceTests
    {
        private Mock<ITransaction> _transaction;
        private Mock<ISession> _session;
        private Mock<INHibernateSession> _nhibernateSession;
        private ExpenseCreationService _sut;

        [SetUp]
        public void Setup()
        {
            _transaction = new Mock<ITransaction>();
            _session = new Mock<ISession>();
            _session.Setup(t => t.BeginTransaction())
                .Returns(_transaction.Object);

            _nhibernateSession = new Mock<INHibernateSession>();
            _nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => _session.Object);

            _sut = new ExpenseCreationService(_nhibernateSession.Object);
        }

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

            var expenseEntity = new ExpenseEntity()
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

            _session.Setup(x => x.Save(It.IsAny<ExpenseEntity>()))
                .Returns(() => expenseEntity);

            _sut.Create(expenseInput);

            _session.Verify(x => x.Save(It.IsAny<ExpenseEntity>()), Times.Once);
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
            
            _sut.Create(expenseInput);

            _transaction.Verify(x => x.Commit(), Times.Once);
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

            var post = _sut.Create(expenseInput);

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
            
            var post = _sut.Create(expenseInput);

            var expectedType = new JsonResult(expenseInput);
            var result = post as JsonResult;

            Assert.IsInstanceOf(expectedType.GetType(), result);
        }
    }
}
