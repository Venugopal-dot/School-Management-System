using School.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace School.Application.DTOs.Student;

public class CreateStudentRequest
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[0-9]{10}$",
    ErrorMessage = "Phone number must contain exactly 10 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int ClassRoomId { get; set; }
}