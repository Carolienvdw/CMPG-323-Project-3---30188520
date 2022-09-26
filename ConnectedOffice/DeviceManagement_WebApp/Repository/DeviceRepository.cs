using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        //This retrieves all the devices as well as their category and zone id's
        public IEnumerable<Device> includeZoneCategory()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return connectedOfficeContext.ToList();
        }

        public bool DeviceExists(Guid? id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }

        public IEnumerable<Category> catInfo()
        {
            return _context.Category.ToList();
        }

        public IEnumerable<Zone> zoneInfo()
        {
            return _context.Zone.ToList();
        }

        public Device CreateID(Device device)
        {
            device.DeviceId = Guid.NewGuid();
            return device;
        }
    }
}
