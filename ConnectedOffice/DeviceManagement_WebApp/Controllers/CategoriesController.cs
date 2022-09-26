using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;

namespace DeviceManagement_WebApp.Controllers
{
    public class CategoriesController : Controller
    {
       
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController( ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Categories:  Get all categories
        public async Task<IActionResult> Index()
        {
            return View(_categoryRepository.GetAll());
        }

        // GET: Categories/Details/5: Show the details of the category
        public async Task<IActionResult> Details(Guid? id)
        {
            return View(_categoryRepository.GetById(id));
        }

        // GET: Categories/Create: It renders the view
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create:  Creates a new category
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            _categoryRepository.CreateID(category);
            _categoryRepository.Add(category);
          
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5: Retrieves singular data based on ID
        public async Task<IActionResult> Edit(Guid? id)
        {

            var category = _categoryRepository.GetById(id);
            return View(category);
        }

        // POST: Categories/Edit/5: Let you edit the category on the database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }
            try
            {
                _categoryRepository.Edit(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_categoryRepository.CategoryExists(id))
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

        // GET: Categories/Delete/5:  Get category by id to be deleted
        public async Task<IActionResult> Delete(Guid? id)
        {
            var category = _categoryRepository.GetById(id);

            return View(category);
        }

        // POST: Categories/Delete/5  Let you delete the category
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            _categoryRepository.Remove(category);
           
            return RedirectToAction(nameof(Index));
        }

       
    }
}
