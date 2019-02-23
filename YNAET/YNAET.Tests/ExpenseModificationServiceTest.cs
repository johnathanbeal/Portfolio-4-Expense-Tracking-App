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

            _expense = new Mock<ExpenseEntity>();
            _expense.Setup(ee => ee.Payee).Returns("Sams Club");
            _expense.Setup(ee => ee.Amount).Returns(6.66m);
            _expense.Setup(ee => ee.Date).Returns(System.DateTime.Today);
            _expense.Setup(ee => ee.Account).Returns("Middleburg Bank");
            _expense.Setup(ee => ee.Repeat).Returns(true);
            _expense.Setup(ee => ee.Impulse).Returns(true);
            _expense.Setup(ee => ee.Memo).Returns("expense was modified");
            _expense.Setup(ee => ee.ColorCode).Returns("Cornflower-Blue");

            _sut = new ExpenseModificationService(_nhibernateSession.Object);

        }

        [Test]
        public void ExpenseShouldChange()
        {
            _sut.Modify(1, expenseInput);
            //DOESN'T WORK
            //_expense.Verify(foo => foo.Payee == "Sams Club");

            _expense.VerifySet(foo => foo.Payee != "Sams Club");
        }
    }
}
