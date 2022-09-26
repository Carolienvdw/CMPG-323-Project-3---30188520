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
    public class DevicesController : Controller
    {
       // private readonly ConnectedOfficeContext _context;
        private readonly IDeviceRepository _deviceRepository;

        public DevicesController( IDeviceRepository deviceRepository)
        {
            //_context = context;
            _deviceRepository = deviceRepository;
        }

        // GET: Devices:  Retrieves all devices as wel as their category and zone id's
        public async Task<IActionResult> Index()
        {
            return View(_deviceRepository.includeZoneCategory());
        }
        

        // GET: Devices/Details/5: Returns a device by searching for its id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (!_deviceRepository.DeviceExists(id))
            {
                return NotFound();
            }
             _deviceRepository.includeZoneCategory();
            

            return View(_deviceRepository.GetById(id));
        }

        // GET: Devices/Create: It renders the view
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_deviceRepository.catInfo(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_deviceRepository.zoneInfo(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create: Creating the data in the database. Needs Create method
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            _deviceRepository.CreateID(device);
            _deviceRepository.Add(device);
            
            return RedirectToAction(nameof(Index));


        }

        // GET: Devices/Edit/5: Retrieves singular data based on ID
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _deviceRepository.GetById(id);
            
            if (device == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_deviceRepository.catInfo(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_deviceRepository.zoneInfo(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // POST: Devices/Edit/5 Let you edit the device on the database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (!_deviceRepository.DeviceExists(id))
            {
                return NotFound();
            }
            try
            {
                _deviceRepository.Edit(device);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_deviceRepository.DeviceExists(id))
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

        // GET: Devices/Delete/5: Get device by id
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _deviceRepository.includeZoneCategory();
            var device = _deviceRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5: Let you delete a device
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = _deviceRepository.GetById(id); 
            _deviceRepository.Remove(device);
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
