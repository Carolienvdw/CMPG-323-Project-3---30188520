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

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly IZoneRepository _zoneRepository;

        public ZonesController(ConnectedOfficeContext context, IZoneRepository zoneRepository)
        {
            _context = context;
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

        // POST: Zones/Create
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
            if (id != zone.ZoneId)
            {
                return NotFound();
            }
            

            try
            {
                _context.Update(zone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        private bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
            
        }
    }
}
