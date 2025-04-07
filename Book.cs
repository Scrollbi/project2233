
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Article { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(50)]
    public string Genre { get; set; }

    public string Description { get; set; }

    [Required]
    public DateTime IssueDate { get; set; }

    [Required]
    public DateTime ReturnDate { get; set; }

    [MaxLength(50)]
    public string Status { get; set; }

    [MaxLength(100)]
    public string Reader { get; set; }
}