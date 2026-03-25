#pragma once
#include "IDevice.h"
#include <string>

class NotifyTimerLock : public IDevice {
private:
    bool locked = false;
    int notifyCount = 0;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
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
    std::string GetSchedule() override { return scheduleStart + " - " + scheduleEnd; }
    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
};