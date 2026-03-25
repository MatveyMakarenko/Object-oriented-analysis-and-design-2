#pragma once
#include "IDevice.h"
#include <ctime>
#include <string>

class EnergyLock : public IDevice {
private:
    bool locked = false;
    double energyUsed = 0.0;
    time_t startTime = 0;
public:
    void Activate() override {
        if (locked) return;
        locked = true;
        startTime = time(nullptr);
    }
    void Deactivate() override {
        if (!locked) return;
        locked = false;
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * 5;
        }
    }
    std::string GetStatus() override { return locked ? "ЗАКРЫТ" : "ОТКРЫТ"; }
    std::string GetEnergyUsage() override { return std::to_string(energyUsed) + " Вт·ч"; }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return "-"; }
};