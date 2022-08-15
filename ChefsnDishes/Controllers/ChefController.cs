using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsnDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsnDishes.Controllers;

public class ChefController : Controller
{
    private DatabaseContext _context;


    public ChefController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("/chefs/all")]
    public IActionResult Index()
    {
        List<Chef> AllChefs = _context.Chefs
        .Include(chef => chef.CreatedDishes)
        .ToList();

        return View("All", AllChefs);
    }

    [HttpGet("/chefs/new")]
    public IActionResult New()
        {
            return View("New");
        }

    [HttpPost("/chefs/create")]
    public IActionResult Create(Chef newChef)
    {
        if (ModelState.IsValid == false) {
            return New();
        }

        _context.Chefs.Add(newChef);
        _context.SaveChanges();

        return RedirectToAction("All");
    }

    // [HttpGet("/chefs/{chefId}")]
    // public IActionResult ViewChef(int chefId)
    // {
    //     Chef? chef = _context.Chefs
    //     .Include()
    //     .FirstOrDefault(chef => chef.ChefId == chefId);
        
        

    //     if (chef == null)
    //     {
    //         return RedirectToAction("All");
    //     }
    //     return View("ViewChef", chef);
    // }

    [HttpPost("/chefs/{deleteId}/delete")]
    public IActionResult Delete(int deleteId)
    {
        Chef? chefToDelete = _context.Chefs.FirstOrDefault(chef => chef.ChefId == deleteId);

        if (chefToDelete != null)
        {
            _context.Chefs.Remove(chefToDelete);
            _context.SaveChanges();
        }

        return RedirectToAction("All");
    }

    [HttpGet("/chefs/{editId}/edit")]
    public IActionResult EditChef(int editId)
    {
        Chef? chef = _context.Chefs.FirstOrDefault(chef => chef.ChefId == editId);

        if (chef == null)
        {
            return RedirectToAction("All");
        }

        return View("Edit", chef);
    }

    [HttpPost("/chefs/{updateId}/update")]
    public IActionResult UpdateChef(int updateId, Chef updatedChef)
    {
        if (ModelState.IsValid == false)
        {
            return EditChef(updateId);
        }

        Chef? dbChef = _context.Chefs.FirstOrDefault(chef => chef.ChefId == updateId);

        if (dbChef == null)
        {
            return RedirectToAction("All");
        }

        dbChef.FirstName = updatedChef.FirstName;
        dbChef.LastName = updatedChef.LastName;
        dbChef.DOB = updatedChef.DOB;
        dbChef.UpdatedAt = DateTime.Now;

        _context.Chefs.Update(dbChef);
        _context.SaveChanges();

        return RedirectToAction("ViewChef", new { chefId = dbChef.ChefId});
    }

    

}