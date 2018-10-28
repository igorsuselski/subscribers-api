using Artists.Subscribers.WebApi.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Subscribers.UnitTests
{
    [TestClass]
    public class MailTests
    {

        [TestMethod]
        public void Test()
        {
            //Arrange
            SubscribersMailService mailService = new SubscribersMailService
            {
                RecipientName = "Igor Sushelski",
                RecipientMailAddress = "igor.suselski@keis.com.mk",
                RecipientID = "",

            };

            //Act
            bool isSent = mailService.SendMail();

            //Assert
            Assert.IsTrue(isSent, "Mail is successfuly sent");
        }
    }
}
