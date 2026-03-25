#pragma once
#include <string>

class IDevice {
public:
    virtual void Activate() = 0;
    virtual void Deactivate() = 0;
    virtual std::string GetStatus() = 0;

    // ⚠️ ДОБАВЛЕНО: Виртуальные методы для функций декораторов
    virtual std::string GetEnergyUsage() { return "0 Вт·ч"; }
    virtual std::string GetNotifyCount() { return "0"; }
    virtual std::string GetSchedule() { return "-"; }

    virtual ~IDevice() {}
};