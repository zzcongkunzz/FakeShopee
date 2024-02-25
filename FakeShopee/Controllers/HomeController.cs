﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FakeShopee.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeShopee.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyDbContext _context;

    public HomeController(ILogger<HomeController> logger, MyDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.UserGroups.ToListAsync();
        // User user = 
        Console.WriteLine(users.Count);
        // users.ForEach(user=>{
        //     Console.WriteLine(user.FullName);
        // });
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}