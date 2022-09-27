using DeviceManagement_WebApp.Models;
using System;

namespace DeviceManagement_WebApp.Repository
{
    //Here any aditional interface definitions are given for zones
    public interface IZoneRepository: IGenericRepository<Zone>
    {
        Zone CreateID(Zone zone);
        public bool ZoneExists(Guid? id);
    }

   
}
