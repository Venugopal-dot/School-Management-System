using School.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace School.Application.DTOs.Student;

public class UpdateStudentRequest
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
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[6-9]\d{9}$",
    ErrorMessage = "Enter a valid 10-digit Indian mobile number.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(250)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int ClassRoomId { get; set; }
}