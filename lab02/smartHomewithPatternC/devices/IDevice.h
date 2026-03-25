#pragma once
#include <string>

class IDevice {
public:
    virtual ~IDevice() = default;

    virtual void Activate() = 0;
    virtual void Deactivate() = 0;
    virtual std::string GetStatus() = 0;

    // Опциональные методы (возвращают "-" если не поддерживаются)
    virtual std::string GetEnergyUsage() { return "-"; }
    virtual std::string GetNotifyCount() { return "-"; }
    virtual std::string GetSchedule() { return "-"; }

    virtual void SetSchedule(std::string start, std::string end) {}
};