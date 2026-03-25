#pragma once
#include "IDevice.h"
#include <string>

class NotifyLight : public IDevice {
private:
    bool isOn = false;
    int notifyCount = 0;
public:
    void Activate() override {
        if (isOn) return;
        isOn = true;
        notifyCount++;
    }
    void Deactivate() override {
        if (!isOn) return;
        isOn = false;
        notifyCount++;
    }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }

    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return std::to_string(notifyCount); }
    std::string GetSchedule() override { return "-"; }
};