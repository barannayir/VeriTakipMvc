﻿using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Models;
using VeriTakipMvc.ViewNodels;

namespace VeriTakipMvc.Data.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly Context _context;
        public DeviceRepository(Context context)
        {
            _context = context;
        }
        
        public Task CreateDevice(Device device)
        {
            _context.Devices.Add(device);
            return _context.SaveChangesAsync();
        }
    

        public Task DeleteDevice(int id)
        {
            var device = _context.Devices.Find(id);
            _context.Devices.Remove(device);
            return _context.SaveChangesAsync();
        }



        public async Task<Device> GetDevice(int id)
        {
            return await _context.Devices.FindAsync(id).AsTask();
        }

        public async  Task<DeviceData> GetDeviceData(int deviceId)
        {
            return await _context.DeviceDatas.FindAsync(deviceId).AsTask();

        }

        public async Task<List<Device>> GetDevicesByCompId(int compId)
        {
            return await _context.Devices.Where(x => x.CompanyId == compId).ToListAsync();
        }
        public async Task<List<DeviceData>> GetDeviceDatas(int compid)
        {
            return await _context.DeviceDatas.Where(x => x.CompanyId == compid).ToListAsync();
        }
        public async Task<IEnumerable<DeviceViewModel>> GetDeviceViewModels(int compId)
        {
            var devices = await _context.Devices.Where(x => x.CompanyId == compId).ToListAsync();
            //var deviceDatas = await _context.DeviceDatas.Where(x => x.CompanyId == compId).ToListAsync();
            var deviceDatas = await _context.DeviceDatas
    .Where(x => x.CompanyId == compId)
    .GroupBy(x => x.DeviceId)
    .Select(g => g.OrderByDescending(x => x.DataDateTime).FirstOrDefault())
    .ToListAsync();


            var deviceViewModels = devices.Select(d => new DeviceViewModel
            {
                DeviceId = d.Id,
                CompanyId = d.CompanyId,
                DeviceName = d.DeviceName,
                DeviceGroup = d.DeviceGroup,
                IsDeviceOnAlert = d.IsDeviceOnAlert,
                TemperatureC = deviceDatas.FirstOrDefault(dd => dd.DeviceId == d.Id)?.TemperatureC ?? 0,
                RelayStatus = deviceDatas.FirstOrDefault(dd => dd.DeviceId == d.Id)?.RelayStatus ?? false,
                IsDeviceConnected = deviceDatas.FirstOrDefault(dd => dd.DeviceId == d.Id)?.DataDateTime > DateTime.Now.AddMinutes(-330)
            });

            return deviceViewModels;
        }

        public async Task<DeviceViewModel> GetDeviceViewModel(int deviceId, int compId)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            var deviceData = await _context.DeviceDatas
         .Where(x => x.DeviceId == deviceId)
         .OrderByDescending(x => x.DataDateTime)
         .FirstOrDefaultAsync();
            if (device.CompanyId == compId)
            {
            var deviceViewModel = new DeviceViewModel
            {
                DeviceId = device.Id,
                CompanyId = device.CompanyId,
                DeviceName = device.DeviceName,
                DeviceGroup = device.DeviceGroup,
                IsDeviceOnAlert = device.IsDeviceOnAlert,
                TemperatureC = deviceData?.TemperatureC ?? 0,
                RelayStatus = deviceData?.RelayStatus ?? false,
                IsDeviceConnected = deviceData?.DataDateTime > DateTime.Now.AddMinutes(-330)

            };
            return deviceViewModel;
            }
            return new DeviceViewModel();
        }
        public Task UpdateDevice(Device device)
        {

            _context.Devices.Update(device);
            return _context.SaveChangesAsync();
        }

        public Task<List<Device>> GetDevices()
        {
            return _context.Devices.ToListAsync();
        }
    }
}
