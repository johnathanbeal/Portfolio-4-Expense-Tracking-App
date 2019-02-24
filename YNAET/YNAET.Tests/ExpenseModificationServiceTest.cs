using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Linq;
using YNAET.Models;
using YNAET.Entities;
using YNAET.Nibernate;
using ISession = NHibernate.ISession;
using YNAET.Services;
using Moq;
using NUnit.Framework;

namespace YNAET.Tests
{


    public class ExpenseModificationServiceTest
    {
        private Mock<ITransaction> _transaction;
        private Mock<ISession> _session;
        private Mock<INHibernateSession> _nhibernateSession;
        private ExpenseModificationService _sut;
        private Mock<ExpenseEntity> _deleteExpense;
        private Random _random;
        private int _randomId;
        private ExpenseInputModel expenseInput;
        private ExpenseEntity expenseEntity;
        private List<ExpenseEntity> expenseEntityList;
        private Mock<ExpenseEntity> _expense;


        [SetUp]
        public void Setup()
        {
            _random = new Random();
            _randomId = _random.Next(1, 3);

            expenseInput = new ExpenseInputModel()
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

            expenseEntity = new ExpenseEntity()
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

            _transaction = new Mock<ITransaction>();
            _session = new Mock<ISession>();
            _session.Setup(t => t.BeginTransaction())
                .Returns(_transaction.Object);

            //_session.Setup(x => x.Query<ExpenseEntity>())
            //    .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            _session.Setup(x => x.Query<ExpenseEntity>())
                 .Returns(() => new List<ExpenseEntity> { expenseEntity }.AsQueryable());

            _nhibernateSession = new Mock<INHibernateSession>();
            _nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => _session.Object);

            _expense = new Mock<ExpenseEntity>();
            _expense.Setup(ee => ee.Account).Returns("Wells Fargo");
            _expense.SetupGet(m => m.Payee).Returns("Someone Nice");
            _expense.Setup(ee => ee.Date).Returns(System.DateTime.Today);
            _expense.Setup(ee => ee.Account).Returns("Middleburg Bank");
            _expense.Setup(ee => ee.Repeat).Returns(true);
            _expense.Setup(ee => ee.Impulse).Returns(true);
            _expense.Setup(ee => ee.Memo).Returns("expense was modified");
            _expense.Setup(ee => ee.ColorCode).Returns("Cornflower-Blue");

            _sut = new ExpenseModificationService(_nhibernateSession.Object);

        }

        [Test]
        public void Modified_Expense_Should_Save()
        {
            
            _sut.Modify(1, expenseInput);

            _session.Verify(x => x.SaveOrUpdate(It.IsAny<ExpenseEntity>()), Times.Once);
        }

        [Test]
        public void Drop_Expense_Should_Commit()
        {
            _sut.Modify(1, expenseInput);

            _transaction.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void Should_Get_Return_Type_Of_ExpenseEntity()
        {
            var expenseResult = _sut.Modify(1, expenseInput);

            Assert.IsInstanceOf(expenseEntity.GetType(), expenseResult);
        }

        [Test]
        public void Should_Not_Have_Null_Type()
        {
            var expenseResult = _sut.Modify(1, expenseInput);

            Assert.IsNotNull(expenseResult);
        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Account()
        {

            _sut.Modify(1, expenseInput);
            _expense.Verify(e => e.Account == "Wells Fargo");
            _expense.VerifySet(e => e.Account = "Wells Fargo");
        }

    }


}
