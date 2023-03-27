namespace VeriTakipMvc.Models
{
    public class DeviceData
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int CompanyId { get; set; }
        public int TemperatureC { get; set; }
        public bool RelayStatus { get; set; }
        public DateTime DataDateTime { get; set; }
        
    }
}
