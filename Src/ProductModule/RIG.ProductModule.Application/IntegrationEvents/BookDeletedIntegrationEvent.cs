using System;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Application.IntegrationEvents
{
    public class BookDeletedIntegrationEvent : IIntegrationMessage
    {
        public string BookId { get; private set; }
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }
        public DateTime DeletedOn { get; private set; }

        public BookDeletedIntegrationEvent(string bookId, string bookName, string authorName, DateTime deletedOn)
        {
            BookId = bookId;
            BookName = bookName;
            AuthorName = authorName;
            DeletedOn = deletedOn;
        }
    }
}