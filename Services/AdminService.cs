﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
using Odem.WebAPI.Models.requests;
using Odem.WebAPI.Utils;

namespace Odem.WebAPI.Services;

public class AdminService : IAdminService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AdminService()
    {
        _context = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserRequest, Admin>();
            cfg.CreateMap<UserRequest, Client>();
            cfg.CreateMap<AddressRequest, Address>();
        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    public Task<Admin> FindAdmin(string email)
    {
        var admin = _context.Admins
            .Include(a => a.HandledTickets)
            .Include(adr=>adr.Address)
            .First(a => a.Email == email);
        return Task.FromResult(admin);
    }

    public Task<Admin> Login(string email,string password)
    {
        var admin = FindAdmin(email);
        if (admin is null || !Crypto.CompareBcrypt(password, admin.Result.Password)) return null!;
        return admin;
    }

    public Task<List<OdemTransfer>> GetTransactions()
    {
        var transactions = _context.OdemTransfers.Include(o=>o.From).Include(o=>o.To).ToList();
        return Task.FromResult(transactions);
    }

    public Task<bool> CreateAdmin(UserRequest admin)
    {
        var newAdmin = _mapper.Map<Admin>(admin);
        newAdmin.Password = Crypto.EncryptBcrypt(admin.Password);
        _context.Admins.Add(newAdmin);
        _context.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<List<Client>> GetClients()
    {
        return Task.FromResult(_context.Clients.Include(c=>c.Address).Include(c=>c.Wallet).ToList());
    }

    public Task<bool> DeleteClient(string email)
    {
        var tmp = _context.Clients.Remove(_context.Clients.First(c => c.Email == email));
        _context.SaveChanges();
        if (tmp.Entity is null)
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    public Task<bool> UpdateClient(UserRequest client)
    {
        var existingClient = _context.Clients.SingleOrDefault(c => c.Email == client.Email);
        if (existingClient == null) return Task.FromResult(false);

        var requestClient = _mapper.Map<Client>(client);
        requestClient.Address = _mapper.Map<Address>(client.Address);
        _context.Clients.Remove(existingClient);
        _context.Clients.Add(requestClient);
        


        _context.SaveChanges();
        return Task.FromResult(true);
    }
}