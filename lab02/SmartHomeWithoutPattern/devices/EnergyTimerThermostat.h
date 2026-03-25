#pragma once
#include "IDevice.h"
#include <ctime>
#include <string>

class EnergyTimerThermostat : public IDevice {
private:
    int temp = 20;
    bool isActive = false;
    double energyUsed = 0.0;
    time_t startTime = 0;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
public:
    void Activate() override {
        if (isActive) return;
        isActive = true;
        startTime = time(nullptr);
    }
    void Deactivate() override {
        if (!isActive) return;
        isActive = false;
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * 100;
        }
    }
    std::string GetStatus() override { return isActive ? std::to_string(temp) + "°C (АКТИВЕН)" : std::to_string(temp) + "°C (ВЫКЛ)"; }
    std::string GetEnergyUsage() override { return std::to_string(energyUsed) + " Вт·ч"; }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return scheduleStart + " - " + scheduleEnd; }
    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
    void SetTemp(int t) { temp = t; }
    int GetCurrentTemp() { return temp; }
};