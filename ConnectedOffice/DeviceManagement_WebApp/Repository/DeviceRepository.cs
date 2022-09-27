using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    //This inherits the IDeviceRepository and then implement the interface
    //Here interface definitions of additional methods are added
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

        //Checks if a device exists
        public bool DeviceExists(Guid? id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }


        //Gives access to the Category information
        public IEnumerable<Category> catInfo()
        {
            return _context.Category.ToList();
        }

        //Gives access to the Zone information
        public IEnumerable<Zone> zoneInfo()
        {
            return _context.Zone.ToList();
        }

        //Creats a new id for a new device
        public Device CreateID(Device device)
        {
            device.DeviceId = Guid.NewGuid();
            return device;
        }
    }
}
