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
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    //Ensures that a person must login first before he can edit anything
    [Authorize]
    public class ZonesController : Controller
    {
        
        private readonly IZoneRepository _zoneRepository;

        public ZonesController( IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        // GET: Zones : Return all zones
        public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());
        }

        // GET: Zones/Details/5: Return Zone by ID
        public async Task<IActionResult> Details(Guid? id)
        {
            return View(_zoneRepository.GetById(id));
        }

        // GET: Zones/Create: It renders the view
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create: Creating the data in the database.
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {

            //it creates a new zone id
            _zoneRepository.CreateID(zone);
            //Creates a new zone
            _zoneRepository.Add(zone);
            

            return RedirectToAction(nameof(Index));

        }

        // GET: Zones/Edit/5 : Retrieves singular data based on ID
        public async Task<IActionResult> Edit(Guid? id)
        {
           
            var zone = _zoneRepository.GetById(id);
            return View(zone);
        }

        // POST: Zones/Edit/5: Let you edit the zone on the database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (! _zoneRepository.ZoneExists(id))
            {
                return NotFound();
            }
            

            try
            {
                _zoneRepository.Edit(zone);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    throw;
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Zones/Delete/5: Get zone by id
        public async Task<IActionResult> Delete(Guid? id)
        {

            var zone = _zoneRepository.GetById(id);

            return View(zone);
        }

        // POST: Zones/Delete/5 : This deletes a zone
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zoneRepository.GetById(id);
            _zoneRepository.Remove(zone);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
