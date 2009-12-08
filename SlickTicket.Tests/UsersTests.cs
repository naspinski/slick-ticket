using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SlickTicket.DomainModel;
using SlickTicket.DomainModel.Objects;

namespace SlickTicket.Tests
{
    [TestFixture]
    public class UsersTests
    {
        [Test]
        public void Non_existant_user_gets_dummy_user()
        {
            user u = User.GetFromEmail("noone@fake-email.com");
            Assert.AreEqual(User.OutsideUser, u.userName);
        }

        //uncomment if you want to test, this may enter a new user into your db based on the email
        //[Test]
        //public void GetFromEmail_makes_new_user_if_needed()
        //{
        //    user u = Users.GetFromEmail("existing_user@your_domain.com");

        //    Assert.AreEqual("318-852-1891", u.phone.Trim());//phone number from AD
        //}
    }
}
