using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;



namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        //public Service GetDeviceZoneCategory()
       // {
         //   return _context.Device.Include(d => d.Category).Include(d => d.Zone);
       // }
    }
}
