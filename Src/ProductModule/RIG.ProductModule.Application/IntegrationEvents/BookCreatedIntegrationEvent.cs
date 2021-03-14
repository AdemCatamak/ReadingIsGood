using System;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Application.IntegrationEvents
{
    public class BookCreatedIntegrationEvent : IIntegrationMessage
    {
        public string BookId { get; private set; }
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public BookCreatedIntegrationEvent(string bookId, string bookName, string authorName, DateTime createdOn)
        {
            BookId = bookId;
            BookName = bookName;
            AuthorName = authorName;
            CreatedOn = createdOn;
        }
    }
}