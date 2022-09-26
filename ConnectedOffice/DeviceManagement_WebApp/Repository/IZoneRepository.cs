using DeviceManagement_WebApp.Models;
using System;

namespace DeviceManagement_WebApp.Repository
{
    public interface IZoneRepository: IGenericRepository<Zone>
    {
        Zone CreateID(Zone zone);
        public bool ZoneExists(Guid? id);
    }

   
}
