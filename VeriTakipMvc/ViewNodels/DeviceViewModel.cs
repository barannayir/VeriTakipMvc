using VeriTakipMvc.Models;

namespace VeriTakipMvc.ViewNodels
{
    public class DeviceViewModel
    {
        public int DeviceId { get; set; }
        public int CompanyId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceGroup { get; set; }
        public bool IsDeviceOnAlert { get; set; }
        public bool IsDeviceInstalled { get; set; } 
        public int TemperatureC { get; set; }
        public bool RelayStatus { get; set; }
        public bool IsDeviceConnected { get; set; }
    }
}
