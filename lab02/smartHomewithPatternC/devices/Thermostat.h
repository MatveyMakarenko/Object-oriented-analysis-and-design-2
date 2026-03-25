#pragma once
#include "IDevice.h"

class Thermostat : public IDevice {
protected:
    int temp = 20;
    bool isActive = false;

public:
    void Activate() override { isActive = true; }
    void Deactivate() override { isActive = false; }
    std::string GetStatus() override {
        return isActive ? std::to_string(temp) + "°C (АКТИВЕН)" : std::to_string(temp) + "°C (ВЫКЛ)";
    }

    void SetTemp(int t) { temp = t; }
    int GetCurrentTemp() { return temp; }
};