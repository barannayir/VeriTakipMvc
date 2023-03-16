namespace VeriTakipMvc.Models
{
    public class Command
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string CommandName { get; set; }
        public string CommandParameter { get; set; }
        public bool CommandStatus { get; set; }
        public DateTime Date { get; set; }
    }
}
