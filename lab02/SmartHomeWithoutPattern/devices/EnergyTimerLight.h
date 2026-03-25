#pragma once
#include "IDevice.h"
#include <ctime>
#include <string>

class EnergyTimerLight : public IDevice {
private:
    bool isOn = false;
    double energyUsed = 0.0;
    time_t startTime = 0;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
public:
    void Activate() override {
        isOn = true;
        startTime = time(nullptr);
    }
    void Deactivate() override {
        isOn = false;
        if (startTime > 0) {
            double hours = difftime(time(nullptr), startTime) / 3600.0;
            energyUsed += hours * 15;
        }
    }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }
    std::string GetEnergyUsage() {
        return std::to_string(energyUsed) + " Вт·ч";
    }
    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
    std::string GetSchedule() {
        return scheduleStart + " - " + scheduleEnd;
    }
};