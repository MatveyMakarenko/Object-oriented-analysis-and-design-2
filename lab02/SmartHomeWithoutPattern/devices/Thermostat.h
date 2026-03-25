#pragma once
#include "IDevice.h"

class Thermostat : public IDevice {
private:
    int temp = 20;
    bool isActive = false;
public:
    void Activate() override {
        if (isActive) return;
        isActive = true;
    }
    void Deactivate() override {
        if (!isActive) return;
        isActive = false;
    }
    std::string GetStatus() override { return isActive ? std::to_string(temp) + "°C (АКТИВЕН)" : std::to_string(temp) + "°C (ВЫКЛ)"; }
    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return "-"; }
    void SetTemp(int t) { temp = t; }
    int GetCurrentTemp() { return temp; }
};