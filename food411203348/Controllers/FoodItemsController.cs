
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using food411203348.Models;
using food411203348.Data;

public class FoodItemsController : Controller
{
    private readonly AppDbContext _context;

    public FoodItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: FOODITEMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.FoodItems.ToListAsync());
    }

    // GET: FOODITEMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fooditem = await _context.FoodItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fooditem == null)
        {
            return NotFound();
        }

        return View(fooditem);
    }

    // GET: FOODITEMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FOODITEMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Category,Price,Calories,Rating")] FoodItem fooditem)
    {
        if (ModelState.IsValid)
        {
            _context.Add(fooditem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fooditem);
    }

    // GET: FOODITEMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fooditem = await _context.FoodItems.FindAsync(id);
        if (fooditem == null)
        {
            return NotFound();
        }
        return View(fooditem);
    }

    // POST: FOODITEMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Category,Price,Calories,Rating")] FoodItem fooditem)
    {
        if (id != fooditem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(fooditem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(fooditem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(fooditem);
    }

    // GET: FOODITEMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fooditem = await _context.FoodItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fooditem == null)
        {
            return NotFound();
        }

        return View(fooditem);
    }

    // POST: FOODITEMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var fooditem = await _context.FoodItems.FindAsync(id);
        if (fooditem != null)
        {
            _context.FoodItems.Remove(fooditem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FoodItemExists(int? id)
    {
        return _context.FoodItems.Any(e => e.Id == id);
    }
}
