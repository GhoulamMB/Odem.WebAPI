﻿using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;

namespace Odem.WebAPI.Services;

public interface IAdminService
{
    public Task<Admin> FindAdmin(string email);
    public Task<Admin> Login(string email,string password);
    public Task<List<OdemTransfer>> GetTransactions();
    public Task<bool> CreateAdmin(UserRequest admin);
}