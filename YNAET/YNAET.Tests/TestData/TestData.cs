using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YNAET.Tests.TestData
{
    public static class TestData
    {
        private static string[] categories = { "Rebalance", "Tax Return", "Offering", "Cell", "Utilities", "Mortgage", "Groceries", "Car Gas", "Trips", "Birthdays", "Celebrations",
      "Christmas", "Kittens", "Preschool", "Car Expenses", "EZ Pass", "Subscriptions", "Stuff I Forgot to Budget For", "Auto Loan", "Student Loan", "Jujitsu/Krav Maga",
      "Swimming", "VA529", "Training Fund", "Sports Gym", "Dining Out", "Fun Money" };

        private static string[] accounts = { "Suntrust", "Middleburg", "Wells Fargo" };

        private static string[] colorCodes = { "Grey", "Black", "Red", "Orange", "yellow", "Green", "Blue", "Purple", "Pink", "Cornflower-Blue" };
        

        public static string RandomAlphaNumericString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmonpqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static Decimal RandomDecimal()
        {
            Random random = new Random();
            var penny = Math.Round(random.Next(11, 99) *.01, 2);
            var dollar = random.Next(1, 9999);
            var amount = dollar + penny;
            decimal randomDollarAmount = Convert.ToDecimal(amount);
            return randomDollarAmount;
        }

        public static string RandomCategory()
        {
            Random random = new Random();
            int index = random.Next(categories.Length);
            return categories[index];
        }

        public static string RandomAccount()
        {
            Random random = new Random();
            int index = random.Next(accounts.Length);
            return accounts[index];
        }

        public static string RandomColorCode()
        {
            Random random = new Random();
            int index = random.Next(colorCodes.Length);
            return colorCodes[index];
        }

        public static bool RandomBoolean()
        {
            Random random = new Random();
            return random.Next(2) == 0;
        }


    }
}

