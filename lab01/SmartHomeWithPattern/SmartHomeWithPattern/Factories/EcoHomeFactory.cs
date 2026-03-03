using Microsoft.VisualBasic.Devices;
using SmartHomeWithPattern.Products;
using System.Collections.Generic;
using SmartHomeWithPattern.Products;

namespace SmartHomeWithPattern.Factories
{
    public class EcoHomeFactory : DeviceFactory
    {
        public Light CreateLight()
        {
            return new EcoLight();
        }

        public Thermostat CreateThermostat()
        {
            return new EcoThermostat();
        }

        public Lock CreateLock()
        {
            return new EcoLock();
        }
    }
}