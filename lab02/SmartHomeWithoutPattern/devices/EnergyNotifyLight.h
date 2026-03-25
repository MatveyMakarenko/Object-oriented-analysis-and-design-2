#pragma once
#include "IDevice.h"
#include <ctime>
#include <string>

class EnergyNotifyLight : public IDevice {
private:
    bool isOn = false;
    double energyUsed = 0.0;
    int notifyCount = 0;
    time_t startTime = 0;
public:
    void Activate() override {
        isOn = true;
        startTime = time(nullptr);
        notifyCount++;
    }
    void Deactivate() override {
        isOn = false;
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * 15;
        }
        notifyCount++;
    }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }
    std::string GetEnergyUsage() {
        return std::to_string(energyUsed) + " Вт·ч";
    }
    std::string GetNotifyCount() {
        return std::to_string(notifyCount);
    }
};