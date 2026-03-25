#pragma once
#include "DeviceDecorator.h"
#include <ctime>
#include <string>

class EnergyDecorator : public DeviceDecorator {
private:
    double energyUsed = 0.0;
    time_t startTime = 0;

    double GetPower() const {
        auto status = device->GetStatus();
        if (status.find("ВКЛ") != std::string::npos ||
            status.find("АКТИВЕН") != std::string::npos ||
            status.find("ЗАКРЫТ") != std::string::npos) {
            if (status.find("°C") != std::string::npos) return 100.0;
            if (status == "ЗАКРЫТ") return 5.0;
            return 15.0;
            }
        return 0.0;
    }

public:
    EnergyDecorator(IDevice* dev) : DeviceDecorator(dev) {}
    ~EnergyDecorator() override = default;

    void Activate() override {
        DeviceDecorator::Activate();
        startTime = time(nullptr);
    }

    void Deactivate() override {
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * GetPower();
        }
        DeviceDecorator::Deactivate();
    }

    std::string GetStatus() override {
        return DeviceDecorator::GetStatus();
    }


    std::string GetEnergyUsage() override {
        return std::to_string(energyUsed) + " Вт·ч";
    }
};