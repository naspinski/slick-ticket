using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SlickTicket.DomainModel;
using SlickTicket.DomainModel.Objects;

namespace SlickTicket.Tests.DomainModel.Objects
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
    }
}
