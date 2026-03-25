#pragma once
#include "DeviceDecorator.h"
#include <iostream>
#include <string>

class NotifyDecorator : public DeviceDecorator {
private:
    int notifyCount = 0;
    bool wasActive = false;

public:
    NotifyDecorator(IDevice* dev) : DeviceDecorator(dev) {}
    ~NotifyDecorator() override = default;

    void Activate() override {
        if (!wasActive) {
            notifyCount++;
            wasActive = true;
            std::cout << "[NOTIFY] Устройство включено\n";
        }
        DeviceDecorator::Activate();
    }

    void Deactivate() override {
        if (wasActive) {
            notifyCount++;
            wasActive = false;
            std::cout << "[NOTIFY] Устройство выключено\n";
        }
        DeviceDecorator::Deactivate();
    }

    std::string GetStatus() override {
        return DeviceDecorator::GetStatus();
    }

    std::string GetNotifyCount() override {
        return std::to_string(notifyCount);
    }
};