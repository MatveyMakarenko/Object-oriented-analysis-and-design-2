#pragma once
#include "IDevice.h"
#include <ctime>
#include <string>

class EnergyNotifyTimerLock : public IDevice {
private:
    bool locked = false;
    double energyUsed = 0.0;
    int notifyCount = 0;
    time_t startTime = 0;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
public:
    void Activate() override {
        if (locked) return;
        locked = true;
        startTime = time(nullptr);
        notifyCount++;
    }
    void Deactivate() override {
        if (!locked) return;
        locked = false;
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * 5;
        }
        notifyCount++;
    }
    std::string GetStatus() override { return locked ? "ЗАКРЫТ" : "ОТКРЫТ"; }
    std::string GetEnergyUsage() override { return std::to_string(energyUsed) + " Вт·ч"; }
    std::string GetNotifyCount() override { return std::to_string(notifyCount); }
    std::string GetSchedule() override { return scheduleStart + " - " + scheduleEnd; }
    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
};