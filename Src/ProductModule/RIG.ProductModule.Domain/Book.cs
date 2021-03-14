using System;
using RIG.ProductModule.Domain.DomainEvents;
using RIG.ProductModule.Domain.Exceptions;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Domain
{
    public class Book : DomainEventHolder
    {
        public BookId Id { get; private set; }
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public Book(string bookName, string authorName)
            : this(new BookId(Guid.NewGuid()), bookName, authorName, DateTime.UtcNow, DateTime.UtcNow)
        {
        }

        private Book(BookId id, string bookName, string authorName, DateTime createdOn, DateTime updatedOn)
        {
            Id = id;
            BookName = bookName;
            AuthorName = authorName;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
        }

        public static Book Create(string bookName, string authorName)
        {
            bookName = bookName?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(bookName)) throw new BookNameEmptyException();
            authorName = authorName?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(authorName)) throw new AuthorNameEmptyException();

            Book book = new Book(bookName, authorName);
            BookCreatedEvent bookCreatedEvent = new BookCreatedEvent(book);
            book.AddDomainEvent(bookCreatedEvent);
            return book;
        }

        public void SetDeleted()
        {
            IsDeleted = true;
            UpdatedOn = DateTime.UtcNow;
            BookDeletedEvent bookDeletedEvent = new BookDeletedEvent(this);
            AddDomainEvent(bookDeletedEvent);
        }
    }
}