#pragma once
#include "IDevice.h"
#include <string>

class NotifyLock : public IDevice {
private:
    bool locked = false;
    int notifyCount = 0;
public:
    void Activate() override {
        if (locked) return;
        locked = true;
        notifyCount++;
    }
    void Deactivate() override {
        if (!locked) return;
        locked = false;
        notifyCount++;
    }
    std::string GetStatus() override { return locked ? "ЗАКРЫТ" : "ОТКРЫТ"; }
    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return std::to_string(notifyCount); }
    std::string GetSchedule() override { return "-"; }
};