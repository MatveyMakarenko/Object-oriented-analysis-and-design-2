#pragma once
#include "IDevice.h"

class Light : public IDevice {
private:
    bool isOn = false;

public:
    void Activate() override { isOn = true; }
    void Deactivate() override { isOn = false; }
    std::string GetStatus() override { return isOn ? "ВКЛ" : "ВЫКЛ"; }
};