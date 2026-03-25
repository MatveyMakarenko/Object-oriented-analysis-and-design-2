#pragma once
#include "IDevice.h"
#include <ctime>
#include <string>

class EnergyLight : public IDevice {
private:
    bool isOn = false;
    double energyUsed = 0.0;
    time_t startTime = 0;
public:
    void Activate() override {
        if (isOn) return;
        isOn = true;
        startTime = time(nullptr);
    }
    void Deactivate() override {
        if (!isOn) return;
        isOn = false;
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * 15;
        }
    }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }

    // ⚠️ Переопределяем только GetEnergyUsage()
    std::string GetEnergyUsage() override {
        return std::to_string(energyUsed) + " Вт·ч";
    }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return "-"; }
};