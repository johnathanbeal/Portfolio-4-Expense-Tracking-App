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
using YNAET.Exceptions;

namespace YNAET.Tests
{
    public class ExpenseDeletionServiceTests
    {
        private Mock<ITransaction> _transaction;
        private Mock<ISession> _session;
        private Mock<INHibernateSession> _nhibernateSession;
        private ExpenseDeletionService _sut;
        private Mock<ExpenseEntity> _deleteExpense;
        private List<ExpenseEntity> expenseEntityList;
        private Random _random;
        private int _randomId;
        

        [SetUp]
        public void Setup()
        {
            _random = new Random();
            _randomId = _random.Next(1, 3);

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

            expenseEntityList = new List<ExpenseEntity>()
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

            _transaction = new Mock<ITransaction>();
            _session = new Mock<ISession>();
            _session.Setup(t => t.BeginTransaction())
                .Returns(_transaction.Object);

            _session.Setup(x => x.Query<ExpenseEntity>())
                .Returns(() => new List<ExpenseEntity> { expenseEntityList.Where(e =>e.Id == _randomId).FirstOrDefault() }.AsQueryable());

            _nhibernateSession = new Mock<INHibernateSession>();
            _nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => _session.Object);
            
            _sut = new ExpenseDeletionService(_nhibernateSession.Object);
        }

        [Test]
        public void Drop_Random_Expense_Should_Commit()
        {          
            _sut.Drop(_randomId);

            _transaction.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void Drop_Random_Expense_Should_Save()
        {
            _sut.Drop(_randomId);

            _session.Verify(x => x.Delete(It.IsAny<ExpenseEntity>()), Times.Once);
        }

        [Test]
        public void Invalid_Expense_Should_Throw_NotFoundException()
        {
            var ex = Assert.Throws<NotFoundException>(() => _sut.Drop(1000000));
            Assert.That(ex.Message, Is.EqualTo("The record was not found"));
            
        }
        
    }
}
