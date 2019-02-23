using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NHibernate;
using Microsoft.AspNetCore.Mvc;
using YNAET.Nibernate;
using YNAET.Services;
using YNAET.Entities;
using System.Linq;

namespace YNAET.Tests
{
    public class ExpenseDeletionServiceTests
    {
        private Mock<ITransaction> _transaction;
        private Mock<ISession> _session;
        private Mock<INHibernateSession> _nhibernateSession;
        private ExpenseDeletionService _sut;
        private Mock<ExpenseEntity> _deleteExpense;
        private Random _random;
        private int _randomId;
        

        [SetUp]
        public void Setup()
        {
            _random = new Random();
            _randomId = _random.Next(1, 3);

            var expenseEntityList = new List<ExpenseEntity>()
            {
                new ExpenseEntity()
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
                },

                new ExpenseEntity
                {
                    Id = 2,
                    Payee = "Amazon",
                    Amount = 22.00M,
                    Category = "Popcorn",
                    Account = "Wells Fargo",
                    Date = DateTime.Today,
                    Repeat = true,
                    Impulse = false,
                    Memo = "Pop Secret Homestyle",
                    ColorCode = "Grey"
                },

                new ExpenseEntity
                {
                Id = 3,
                Payee = "Alamo Drafthouse",
                Amount = 7.00M,
                Category = "Fun Money",
                Account = "Middleburg Bank",
                Date = DateTime.Today,
                Repeat = false,
                Impulse = false,
                Memo = "Captain Marvel Tickets",
                ColorCode = "Red"
                }
             };

            //var expenseEntity = new ExpenseEntity()
            //{
            //    Id = 10,
            //    Payee = "Amazon",
            //    Amount = 18.00M,
            //    Category = "Stuff I Forget to Budget For",
            //    Account = "Middleburg",
            //    Date = DateTime.Today,
            //    Repeat = false,
            //    Impulse = true,
            //    Memo = "Dry Erase Paper Sheets",
            //    ColorCode = "Blue"
            //};

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

            _transaction = new Mock<ITransaction>();
            _session = new Mock<ISession>();
            _session.Setup(t => t.BeginTransaction())
                .Returns(_transaction.Object);

            _session.Setup(x => x.Query<ExpenseEntity>())
                .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            _nhibernateSession = new Mock<INHibernateSession>();
            _nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => _session.Object);
            
            _sut = new ExpenseDeletionService(_nhibernateSession.Object);

        }
       // [Test]
        public void DropReducesCountByOne()
        {
            
        }

        [Test]
        public void DropExpenseShouldCommit()
        {
            
            
            _sut.Drop(1);

            _transaction.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void DropExpenseShouldSave()
        {
           
            _sut.Drop(1);

            _session.Verify(x => x.Delete(It.IsAny<ExpenseEntity>()), Times.Once);
        }

        [Test]
        public void ExpenseWithInvalidIdShouldReturnNotFound()
        {
            var result = _sut.Drop(1000000);
            var notFoundResult = new Microsoft.AspNetCore.Mvc.NotFoundResult();
            Assert.IsInstanceOf(notFoundResult.GetType(), result);
        }

        [Test]
        public void ExpenseValidIdShouldReturnOkResult()
        {

            var result = _sut.Drop(1);
            var okResult = new OkResult();
            Assert.IsInstanceOf(okResult.GetType(), result);
        }
    }
}
