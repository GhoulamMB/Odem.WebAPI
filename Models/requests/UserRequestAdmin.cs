﻿namespace Odem.WebAPI.Models.requests;

public class UserRequestAdmin
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Password { get; set; }
    public required AddressRequest Address { get; set; }
}