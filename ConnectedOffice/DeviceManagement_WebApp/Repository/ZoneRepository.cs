using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    //This inherits the IServiceRepository and then implement the interface
    //Here interface definitions of additional methods are added
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {

        public ZoneRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        //This creates a new id for a zone
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
