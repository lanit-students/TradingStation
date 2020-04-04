using System;

using Interfaces;
using DTO.NewsRequests;

using CBIntegration;

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
                    return new RussianCBInfo();
                default:
                    //TODO change on custom exception
                        throw new NotImplementedException();
            }
        }
    }
}
