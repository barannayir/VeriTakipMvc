using VeriTakipMvc.Models;

namespace VeriTakipMvc.ViewNodels
{
    public class AdminDeviceVM
    {
        public IEnumerable<Device> Devices { get; set; }
        public Device Device { get; set; }
    }
}
