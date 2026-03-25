#pragma once
#include "IDevice.h"

class Lock : public IDevice {
private:
    bool locked = false;

public:
    void Activate() override { locked = true; }
    void Deactivate() override { locked = false; }
    std::string GetStatus() override { return locked ? "ЗАКРЫТ" : "ОТКРЫТ"; }
};