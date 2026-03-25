#pragma once
#include "IDevice.h"

class Light : public IDevice {
private:
    bool isOn = false;
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

    // Возвращаем значения по умолчанию
    std::string GetEnergyUsage() override { return "0 Вт·ч"; }
    std::string GetNotifyCount() override { return "0"; }
    std::string GetSchedule() override { return "-"; }
};