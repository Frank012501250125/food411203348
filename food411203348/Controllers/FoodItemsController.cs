using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // 💡新增這行：為了使用 SelectList (下拉選單)
using Microsoft.EntityFrameworkCore;
using food411203348.Models;
using food411203348.Data;
using System.Linq;
using System.Threading.Tasks;

namespace food411203348.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly AppDbContext _context;

        public FoodItemsController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 💡 查詢功能
        // ==========================================
        // GET: FOODITEMS
        public async Task<IActionResult> Index(string searchString)
        {
            // 💡 新增這行：把使用者輸入的關鍵字存起來，傳回給網頁，讓搜尋框不會變空白
            ViewData["CurrentFilter"] = searchString;

            // 1. 跨表查詢關鍵：加上 .Include(f => f.Category) 把分類資料一起抓出來
            var foods = from f in _context.FoodItems.Include(f => f.Category)
                        select f;

            // 2. 如果使用者有輸入關鍵字，就篩選「食物名稱」
            if (!string.IsNullOrEmpty(searchString))
            {
                foods = foods.Where(s => s.Name.Contains(searchString));
            }

            // 3. 回傳篩選後的結果給網頁
            return View(await foods.ToListAsync());
        }
        // ==========================================

        // GET: FOODITEMS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 💡 詳細資料頁面也要加上 .Include()
            var fooditem = await _context.FoodItems
                .Include(f => f.Category)
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
            // 💡 準備「分類」的下拉選單資料給網頁用
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: FOODITEMS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Price,Calories,Rating")] FoodItem fooditem) // 💡 Category 改成 CategoryId
        {
            if (ModelState.IsValid)
            {
                _context.Add(fooditem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", fooditem.CategoryId);
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
            // 💡 準備下拉選單，並預設選中原本的分類
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", fooditem.CategoryId);
            return View(fooditem);
        }

        // POST: FOODITEMS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,CategoryId,Price,Calories,Rating")] FoodItem fooditem) // 💡 Category 改成 CategoryId
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", fooditem.CategoryId);
            return View(fooditem);
        }

        // GET: FOODITEMS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 💡 刪除確認頁面也要加上 .Include()
            var fooditem = await _context.FoodItems
                .Include(f => f.Category)
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
}