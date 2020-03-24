using Interfaces;
using CentralBankIntegration;
using System;

namespace NewsService.Utils
{
    public static class NewsPublisherFactory
    {
        // This method create an instance of INewsPublisher depending on the newsPublisherType

        public static INewsPublisher Create(NewsPublisherTypes newsPublisherType)
        {
            switch (newsPublisherType)
            {
                case NewsPublisherTypes.CentralBank:
                    return new CentralBankNewsPublisher();
                default:
                        throw new NotImplementedException();
            }
        }
    }
}
