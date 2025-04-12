using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPP.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagerAPP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext context;

    public HomeController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View( await context.Tasks.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        if (id == null || context.Tasks == null)
        {
            NotFound("Task Not Available");
        }
        var data = await context.Tasks.FindAsync(id);

        if(data == null)
        {
            return NotFound("Task Already Completed");
        }
        return View(data);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Task task)
    {
        if (ModelState.IsValid)
        {
            task.due_Date = DateTime.SpecifyKind(task.due_Date, DateTimeKind.Utc);
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return (RedirectToAction("Index", "Home"));
        }
        return View(task);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var data = await context.Tasks.FindAsync(id);

        return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Models.Task task)
    {
        if(id != task.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            task.due_Date = DateTime.SpecifyKind(task.due_Date, DateTimeKind.Utc);
            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        return View();
    }


    // GET: Task/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var task = await context.Tasks.FindAsync(id);

        // Check if the task exists
        if (task == null)
        {
            return NotFound();  // Return 404 if task doesn't exist
        }

        return View(task);  // Show confirmation view
    }

    // POST: Task/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();  // Return 404 if task is not found (it may have been deleted already)
        }

        context.Tasks.Remove(task);  // Remove the task
        await context.SaveChangesAsync();  // Save changes to the database

        return RedirectToAction(nameof(Index));  // Redirect to Index action of the Task controller
    }

}
