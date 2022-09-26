using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {

        public IEnumerable<Device> includeZoneCategory();
        public bool DeviceExists(Guid? id);
        public IEnumerable<Category> catInfo();
        public IEnumerable<Zone> zoneInfo();
        public Device CreateID(Device device);
    }

}
