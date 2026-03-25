#pragma once
#include "IDevice.h"
#include <string>

class NotifyTimerLight : public IDevice {
private:
    bool isOn = false;
    int notifyCount = 0;
    std::string scheduleStart = "";
    std::string scheduleEnd = "";
public:
    void Activate() override {
        isOn = true;
        notifyCount++;
    }
    void Deactivate() override {
        isOn = false;
        notifyCount++;
    }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }
    std::string GetNotifyCount() {
        return std::to_string(notifyCount);
    }
    void SetSchedule(std::string start, std::string end) {
        scheduleStart = start;
        scheduleEnd = end;
    }
    std::string GetSchedule() {
        return scheduleStart + " - " + scheduleEnd;
    }
};