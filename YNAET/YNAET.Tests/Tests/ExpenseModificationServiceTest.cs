using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using YNAET.Tests.TestData;
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
        private ExpenseEntity modifiedExpense;
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

            modifiedExpense = new ExpenseEntity()
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
                 .Returns(() => new List<ExpenseEntity> { modifiedExpense }.AsQueryable());

            _nhibernateSession = new Mock<INHibernateSession>();
            _nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => _session.Object);

            _expense = new Mock<ExpenseEntity>();
            //_session.Setup(It.Is<ExpenseEntity>(e => e.Account)).Returns("Wells Fargo");
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
        public void Modify_Expense_Should_Commit()
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
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = "Amazon",
                Amount = 100.00M,
                Category = "Fun Money",
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Payee()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = 100.00M,
                Category = "Fun Money",
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Amount()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = "Fun Money",
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Category()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Date()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = false,
                Impulse = true,
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Repeat()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = TestData.TestData.RandomBoolean(),
                Impulse = true,
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Impulse()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = TestData.TestData.RandomBoolean(),
                Impulse = TestData.TestData.RandomBoolean(),
                Memo = "Closing Time",
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Memo()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = TestData.TestData.RandomBoolean(),
                Impulse = TestData.TestData.RandomBoolean(),
                Memo = TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10),
                ColorCode = "Pink"

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_ColorCode()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = TestData.TestData.RandomBoolean(),
                Impulse = TestData.TestData.RandomBoolean(),
                Memo = TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10),
                ColorCode = TestData.TestData.RandomColorCode(),

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }

        [Test]
        public void Modified_Expense_Should_Have_Different_Values()
        {
            var expenseInput = new ExpenseInputModel()
            {
                Id = 1,
                Payee = TestData.TestData.RandomAlphaNumericString(8),
                Amount = TestData.TestData.RandomDecimal(),
                Category = TestData.TestData.RandomCategory(),
                Account = TestData.TestData.RandomAccount(),
                Date = DateTime.Today,
                Repeat = TestData.TestData.RandomBoolean(),
                Impulse = TestData.TestData.RandomBoolean(),
                Memo = TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10)
                + " " + TestData.TestData.RandomAlphaNumericString(10),
                ColorCode = TestData.TestData.RandomColorCode(),

            };

            _sut.Modify(1, expenseInput);
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Payee == expenseInput.Payee)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Amount == expenseInput.Amount)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Account == expenseInput.Account)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Category == expenseInput.Category)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Date == expenseInput.Date)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Repeat == expenseInput.Repeat)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Impulse == expenseInput.Impulse)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.Memo == expenseInput.Memo)));
            _session.Verify(x => x.SaveOrUpdate(It.Is<ExpenseEntity>(y => y.ColorCode == expenseInput.ColorCode)));

        }
    }


}
