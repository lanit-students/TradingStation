using DTO.RestRequests;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Linq;
using UserService.Validators;

namespace UserServiceTests.Validators
{
    public class UserInfoRequestValidatorTests
    {
        private IValidator<UserInfoRequest> validator = new UserInfoRequestValidator();

        [Test]
        public void AllFieldsEmpty()
        {
            var user = new UserInfoRequest();

            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Test]
        public void MissUserId()
        {
            var user = new UserInfoRequest
            {
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.IdIsNullOrEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyUserId()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.Empty,
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.IdIsNullOrEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissEmail()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullEmail()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = null,
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyEmail()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.EmailEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void InvalidEmail()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla.bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };
            
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.InvalidEmail);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissFirstName()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void NullFirstName()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = null,
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void EmptyFirstName()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameContainsNumbers()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo12",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameError);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameFirstLetterIsNotUpperCase()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameError);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameRestLettersAreNotLowerCase()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "LeO",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameError);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameTooLong()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "C" + new string('c', 40),
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameTooLong);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void FirstNameConsistOnlyOneLetter()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "L",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.FirstNameTooShort);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissLastName()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void LastNameContainsNumbers()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "Kor12",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameError);

            Assert.IsNotNull(expectedError);
        }

        
        [Test]
        public void LastNameFirstLetterIsNotUpperCase()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameError);

            Assert.IsNotNull(expectedError);
        }        

        [Test]
        public void LastNameRestLettersAreNotLowerCase()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "KoR",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameError);

            Assert.IsNotNull(expectedError);
        }


        [Test]
        public void LastNameTooLong()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "C" + new string('c', 40),
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameTooLong);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void LastNameConsistOnlyOneLetter()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "K",
                Birthday = DateTime.Today.AddYears(-19)
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.LastNameTooShort);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void MissBirthday()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "Kor",
            };

            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdayEmpty);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void WrongBirthday()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Now.AddDays(1),
            };
            
            var validationResult = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));

            var expectedError =
                validationResult.Errors.FirstOrDefault(
                    error => error.ErrorMessage == ErrorsMessages.BirthdayYoung);

            Assert.IsNotNull(expectedError);
        }

        [Test]
        public void ValidData()
        {
            var user = new UserInfoRequest
            {
                UserId = Guid.NewGuid(),
                Email = "bla@bla.com",
                FirstName = "Leo",
                LastName = "Kor",
                Birthday = DateTime.Today.AddYears(-19)
            };

            Assert.DoesNotThrow(() => validator.ValidateAndThrow(user));
        }

    }
}
