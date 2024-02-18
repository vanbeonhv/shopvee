﻿using System.ComponentModel.DataAnnotations.Schema;



namespace Models;
[Table("user")]
public class User : AuditEntity
{
    [Column("email")]
    public string Email { get; set; } = null!;
    [Column("password")]
    public string Password {get; set;} = null!;
    [Column("age")]
    public int? Age {get; set;}
    [Column("address")]
    public string? Address {get; set;}
    [Column("refresh_token")] 
    public string RefreshToken { get; set; } = null!;
    [Column("refresh_token_expired")]
    public DateTime RefreshTokenExpired {get; set;}
    [Column("access_token")]
    public string? AccessToken {get; set;}
    [Column("full_name")]
    public string? FullName {get; set;}
    [Column("date_of_birth")]
    public DateTime? DateOfBirth {get; set;}
    [Column("phone")]
    public string? Phone {get; set;}
}
