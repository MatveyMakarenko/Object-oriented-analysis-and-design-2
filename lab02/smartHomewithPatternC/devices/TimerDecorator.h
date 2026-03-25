#pragma once
#include "DeviceDecorator.h"
#include <string>

class TimerDecorator : public DeviceDecorator {
private:
    std::string scheduleStart = "08:00";
    std::string scheduleEnd = "22:00";

public:
    TimerDecorator(IDevice* dev) : DeviceDecorator(dev) {}
    ~TimerDecorator() override = default;

    void Activate() override {
        DeviceDecorator::Activate();
    }

    void Deactivate() override {
        DeviceDecorator::Deactivate();
    }

    std::string GetStatus() override {
        return DeviceDecorator::GetStatus();
    }

    std::string GetSchedule() override {
        return scheduleStart + " — " + scheduleEnd;
    }

    void SetSchedule(std::string start, std::string end) override {
        scheduleStart = start;
        scheduleEnd = end;
    }
};