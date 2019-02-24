using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YNAET.Tests.TestData
{
    public class TestData
    {
        private string[] categories = { "Rebalance", "Tax Return", "Offering", "Cell", "Utilities", "Mortgage", "Groceries", "Car Gas", "Trips", "Birthdays", "Celebrations",
      "Christmas", "Kittens", "Preschool", "Car Expenses", "EZ Pass", "Subscriptions", "Stuff I Forgot to Budget For", "Auto Loan", "Student Loan", "Jujitsu/Krav Maga",
      "Swimming", "VA529", "Training Fund", "Sports Gym", "Dining Out", "Fun Money" };

        private string[] accounts = { "Suntrust", "Middleburg", "Wells Fargo" };

        private string[] colorCodes = { "Grey", "Black", "Red", "Orange", "yellow", "Green", "Blue", "Purple", "Pink", "Cornflower-Blue" };
        

        public string RandomAlphaNumericString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmonpqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomCategory()
        {
            Random random = new Random();
            int index = random.Next(categories.Length);
            return categories[index];
        }

        public string RandomAccount()
        {
            Random random = new Random();
            int index = random.Next(accounts.Length);
            return accounts[index];
        }

        public string RandomColorCode()
        {
            Random random = new Random();
            int index = random.Next(colorCodes.Length);
            return colorCodes[index];
        }


    }
}

