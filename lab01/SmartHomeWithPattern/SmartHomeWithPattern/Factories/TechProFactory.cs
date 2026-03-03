using Microsoft.VisualBasic.Devices;
using SmartHomeWithPattern.Products;
using System.Collections.Generic;

namespace SmartHomeWithPattern.Factories
{
    public class TechProFactory : DeviceFactory
    {
        public Light CreateLight()
        {
            return new TechLight();
        }

        public Thermostat CreateThermostat()
        {
            return new TechThermostat();
        }

        public Lock CreateLock()
        {
            return new TechLock();
        }
    }
}