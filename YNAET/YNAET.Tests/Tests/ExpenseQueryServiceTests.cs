﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using NHibernate;
using System.Linq;
using YNAET.Entities;
using YNAET.Nibernate;
using YNAET.Services;

namespace YNAET.Tests
{
    [TestFixture]
    public class ExpenseQueryServiceTests
    {

        [Test]
        public void Should_Get_Return_Type_Of_ExpenseEntity()
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

            var expenseData = new ExpenseQueryService(nhibernateSession.Object);

            var result = expenseData.Query(1);
            Assert.IsInstanceOf(expenseEntity.GetType(), result);
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

            var expenseData = new ExpenseQueryService(nhibernateSession.Object);

            var result = expenseData.Query(1);
            Assert.IsNotNull(result);

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

            var expenseData = new ExpenseQueryService(nhibernateSession.Object);

            var result = expenseData.Query(1);
            Assert.AreEqual(result, expenseEntity);
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

            var expenseData = new ExpenseQueryService(nhibernateSession.Object);

            var result = expenseData.Query(1);
            Assert.AreSame(result, expenseEntity);
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

            var expenseData = new ExpenseQueryService(nhibernateSession.Object);

            var result = expenseData.Query(1);
            Assert.AreEqual(result, expenseEntity);
        }

        [Test]
        public void Should_Have_List_Of_Expenses()
        {
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


            var session = new Mock<ISession>();
            session.Setup(x => x.Query<ExpenseEntity>())
                .Returns(() => new List<ExpenseEntity>(expenseEntityList) { }.AsQueryable());

            var nhibernateSession = new Mock<INHibernateSession>();
            nhibernateSession.Setup(x => x.OpenSession())
                .Returns(() => session.Object);

            var expenseData = new ExpenseQueryService(nhibernateSession.Object);

            var result = expenseData.QueryAll();
            Assert.AreEqual(expenseEntityList, result);

        }
    }
}