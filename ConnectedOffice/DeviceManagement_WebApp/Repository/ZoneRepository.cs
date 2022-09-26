using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Zone CreateID(Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            return zone;
        }

        //Checks if a zone exists
        public bool ZoneExists(Guid? id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }


    }

}
