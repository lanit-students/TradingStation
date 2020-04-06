﻿using System;
using System.Collections.Generic;

using NUnit.Framework;
using FluentValidation;

using DTO.NewsRequests.Currency;
using DTO.NewsRequests;
using NewsService.Validators;
using System.Linq;

namespace NewsServiceTests.Validators
{
    public class CurrencyRequestValidatorTests
    {
        private CurrencyRequest NullRequest = null;

        private CurrencyRequest NullCodesRequest = new CurrencyRequest
        {
            CurrencyPublisher = NewsPublisherTypes.CentralBank,
            CurrencyCodes = null
        };

        private CurrencyRequest EmptyCodesRequest = new CurrencyRequest
        {
            CurrencyPublisher = NewsPublisherTypes.CentralBank,
            CurrencyCodes = new List<string>()
        };

        private IValidator<CurrencyRequest> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new CurrencyRequestValidator();
        }

        [Test]
        public void NullRequestValidate()
        {
            Assert.Throws<ArgumentNullException>(() => validator.ValidateAndThrow(NullRequest));
        }

        [Test]
        public void NullCodesRequestValidate()
        {
            Assert.Throws<NullReferenceException>(() => validator.ValidateAndThrow(NullCodesRequest));
        }

        [Test]
        public void EmptyCodesRequestValidate()
        {
            var ex = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(EmptyCodesRequest));
            Assert.AreEqual("You must set currency codes.", ex.Errors.FirstOrDefault().ErrorMessage);
        }
    }
}
