#pragma once
#include "IDevice.h"

class Lock : public IDevice {
private:
    bool locked = false;
public:
    void Activate() override {
        if (locked) return;
        locked = true;
    }
    void Deactivate() override {
        if (!locked) return;
        locked = false;
    }
    std::string GetStatus() override { return locked ? "ЗАКРЫТ" : "ОТКРЫТ"; }
    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return "-"; }
};