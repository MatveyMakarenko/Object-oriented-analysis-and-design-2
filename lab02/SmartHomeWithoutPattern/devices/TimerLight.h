#pragma once
#include "IDevice.h"
#include <string>

class TimerLight : public IDevice {
private:
    bool isOn = false;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
public:
    void Activate() override {
        if (isOn) return;
        isOn = true;
    }
    void Deactivate() override {
        if (!isOn) return;
        isOn = false;
    }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }

    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return scheduleStart + " - " + scheduleEnd; }

    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
};