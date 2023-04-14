using AutoMapper;
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
}