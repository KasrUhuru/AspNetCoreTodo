using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        [Required]
        public string? Title {get;set;}

        [Required]
        public DateTimeOffset DueAt {get;set;} = DateTimeOffset.Now.AddDays(3);
        public Guid Id { get; set; }

        public bool IsDone {get;set;}

        public string? UserId {get;set;}
    }
}