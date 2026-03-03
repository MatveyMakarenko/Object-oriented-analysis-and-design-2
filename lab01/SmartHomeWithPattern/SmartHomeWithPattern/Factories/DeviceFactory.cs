using Microsoft.VisualBasic.Devices;
using SmartHomeWithPattern.Products;
using System.Collections.Generic;
using SmartHomeWithPattern.Products;

namespace SmartHomeWithPattern.Factories
{
    public interface DeviceFactory
    {
        Light CreateLight();
        Thermostat CreateThermostat();
        Lock CreateLock();
    }
}