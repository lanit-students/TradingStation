
using Interfaces;

using RssIntegrationLib;
using NewsService.Enums;
using Kernel.CustomExceptions;

namespace NewsService.Utils
{
    public static class NewsPublisherFactory
    {
        // This method create an instance of INewsPublisher depending on the newsPublisherType

        public static INewsPublisher Create(NewsPublisherTypes newsPublisherType)
        {
            return newsPublisherType switch
            {
                NewsPublisherTypes.Rambler =>
                    new RamblerRssReader(),
                _ =>
                    throw new BadRequestException("Invalid news publisher type.")
            };
        }
    }
}
