#pragma once
#include "IDevice.h"
#include <string>

class DeviceDecorator : public IDevice {
protected:
    IDevice* device;  // Декорируемый объект

public:
    DeviceDecorator(IDevice* dev) : device(dev) {}

    // НЕ удаляем device здесь! Управление памятью — снаружи
    ~DeviceDecorator() override = default;
    void Activate() override {
        if (device) device->Activate();
    }

    void Deactivate() override {
        if (device) device->Deactivate();
    }

    std::string GetStatus() override {
        if (device) return device->GetStatus();
        return "UNKNOWN";
    }

    // Без этого вызовы "проваливаются" в дефолтную реализацию IDevice

    std::string GetEnergyUsage() override {
        if (device) return device->GetEnergyUsage();
        return "-";
    }

    std::string GetNotifyCount() override {
        if (device) return device->GetNotifyCount();
        return "-";
    }

    std::string GetSchedule() override {
        if (device) return device->GetSchedule();
        return "-";
    }

    void SetSchedule(std::string start, std::string end) override {
        if (device) device->SetSchedule(start, end);
    }
};