using VeriTakipMvc.Models;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Data.Interfaces
{
    public interface IDeviceRepository
    {
        Task CreateDevice(Device device);
        Task DeleteDevice(int id);
        Task<Device> GetDevice(int id);
        Task<List<Device>> GetDevices();
        Task<List<Device>> GetDevicesByCompId(int compId);
        Task UpdateDevice(Device device);

        public Task<List<DeviceData>> GetDeviceDatas(int compid);
        Task<DeviceData> GetDeviceData(int deviceId);
        Task<IEnumerable<DeviceViewModel>> GetDeviceViewModels(int compId);
        public Task<DeviceViewModel> GetDeviceViewModel(int deviceId, int compId);

    }
}
