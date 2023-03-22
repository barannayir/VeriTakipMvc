namespace VeriTakipMvc.Models
{
    public class Device
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceGroup { get; set; }
        public bool IsDeviceOnAlert { get; set; }

    }
}
