﻿using System.Runtime.Serialization;

namespace WebApplication1.DomainModels
{
    //[DataContract(IsReference = true)]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public Author Author { get; set; }
    }
}
