using Artists.Subscribers.Data.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Subscribers.UnitTests.Properties
{
    [TestClass]
    public class ValidationMetodsTest
    {
        [TestMethod]
        public void ValidSubscriberEmail()
        {
            //Arrange
            string mailValid = "john_smith@yahoo.com";        
            bool isValidEmail;
           
            //Act
            isValidEmail = ValidationMetods.ValidSubscriberEmail(mailValid);
          
            //Assert
            Assert.IsTrue(isValidEmail, "Validation metod dasen't validate correct email format");
          
        }

        [TestMethod]
        public void NotValidSubscriberEmail()
        {
            //Arrange
            string notValid = "igor.suselski@yahoocom";
            bool isNotValidEmail;

            //Act
            isNotValidEmail = ValidationMetods.ValidSubscriberEmail(notValid);

            //Assert          
            Assert.IsFalse(isNotValidEmail, "Validation metod dasen't validate incorrect email format");
        }

        [TestMethod]
        public void ValidSubscriberName()
        {
            //Arrange
            string notValidNameNumber = "Igor Su3Sejid";
            string notValidNameSpecial = "Sonja#Stefanovska";
            string validName = "Aleks Smith";
            bool isNotValidNameNr;
            bool isNotValidNameSp;
            bool isValidName;

            //Act
            isNotValidNameNr = ValidationMetods.ValidSubscriberName(notValidNameNumber);
            isNotValidNameSp = ValidationMetods.ValidSubscriberName(notValidNameSpecial);
            isValidName = ValidationMetods.ValidSubscriberName(validName);

            //Assert          
            Assert.IsFalse(isNotValidNameNr, "Validation metod dasen't validate incorrect name format that contains numbers");
            Assert.IsFalse(isNotValidNameSp, "Validation metod dasen't validate incorrect name format that contains special char");
            Assert.IsTrue(isValidName, "Validation metod dasen't validate valid name format");
        }


    }
}
