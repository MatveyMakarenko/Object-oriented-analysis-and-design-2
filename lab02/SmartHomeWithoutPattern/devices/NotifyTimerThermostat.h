#pragma once
#include "IDevice.h"
#include <string>

class NotifyTimerThermostat : public IDevice {
private:
    int temp = 20;
    bool isActive = false;
    int notifyCount = 0;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
public:
    void Activate() override {
        if (isActive) return;
        isActive = true;
        notifyCount++;
    }
    void Deactivate() override {
        if (!isActive) return;
        isActive = false;
        notifyCount++;
    }
    std::string GetStatus() override { return isActive ? std::to_string(temp) + "°C (АКТИВЕН)" : std::to_string(temp) + "°C (ВЫКЛ)"; }
    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return std::to_string(notifyCount); }
    std::string GetSchedule() override { return scheduleStart + " - " + scheduleEnd; }
    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
    void SetTemp(int t) { temp = t; }
    int GetCurrentTemp() { return temp; }
};