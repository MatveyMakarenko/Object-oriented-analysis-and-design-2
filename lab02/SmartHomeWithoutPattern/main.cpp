// ⚠️ НЕ добавлять #define CPPHTTPLIB_OPENSSL_SUPPORT!

#include "httplib.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include <mutex>

// ==================== ПОДКЛЮЧАЕМ ВСЕ 24 КЛАССА ====================
// Light (8 классов)
#include "devices/Light.h"
#include "devices/EnergyLight.h"
#include "devices/NotifyLight.h"
#include "devices/TimerLight.h"
#include "devices/EnergyNotifyLight.h"
#include "devices/EnergyTimerLight.h"
#include "devices/NotifyTimerLight.h"
#include "devices/EnergyNotifyTimerLight.h"

// Thermostat (8 классов)
#include "devices/Thermostat.h"
#include "devices/EnergyThermostat.h"
#include "devices/NotifyThermostat.h"
#include "devices/TimerThermostat.h"
#include "devices/EnergyNotifyThermostat.h"
#include "devices/EnergyTimerThermostat.h"
#include "devices/NotifyTimerThermostat.h"
#include "devices/EnergyNotifyTimerThermostat.h"

// Lock (8 классов)
#include "devices/Lock.h"
#include "devices/EnergyLock.h"
#include "devices/NotifyLock.h"
#include "devices/TimerLock.h"
#include "devices/EnergyNotifyLock.h"
#include "devices/EnergyTimerLock.h"
#include "devices/NotifyTimerLock.h"
#include "devices/EnergyNotifyTimerLock.h"

// ==================== КОНФИГУРАЦИЯ ====================
struct DeviceConfig {
    bool energy = false;
    bool notify = false;
    bool timer = false;
};

// Глобальные устройства
IDevice* light = nullptr;
IDevice* thermo = nullptr;
IDevice* lockDevice = nullptr;  // ⚠️ Переименовано из lock

DeviceConfig lightConfig, thermoConfig, lockConfig;
std::mutex deviceMutex;

// ==================== ФАБРИКА УСТРОЙСТВ ====================
IDevice* createLight(const DeviceConfig& config) {
    if (config.energy && config.notify && config.timer) {
        return new EnergyNotifyTimerLight();
    }
    else if (config.energy && config.notify) {
        return new EnergyNotifyLight();
    }
    else if (config.energy && config.timer) {
        return new EnergyTimerLight();
    }
    else if (config.notify && config.timer) {
        return new NotifyTimerLight();
    }
    else if (config.energy) {
        return new EnergyLight();
    }
    else if (config.notify) {
        return new NotifyLight();
    }
    else if (config.timer) {
        return new TimerLight();
    }
    else {
        return new Light();
    }
}

IDevice* createThermostat(const DeviceConfig& config) {
    if (config.energy && config.notify && config.timer) {
        return new EnergyNotifyTimerThermostat();
    }
    else if (config.energy && config.notify) {
        return new EnergyNotifyThermostat();
    }
    else if (config.energy && config.timer) {
        return new EnergyTimerThermostat();
    }
    else if (config.notify && config.timer) {
        return new NotifyTimerThermostat();
    }
    else if (config.energy) {
        return new EnergyThermostat();
    }
    else if (config.notify) {
        return new NotifyThermostat();
    }
    else if (config.timer) {
        return new TimerThermostat();
    }
    else {
        return new Thermostat();
    }
}

IDevice* createLock(const DeviceConfig& config) {
    if (config.energy && config.notify && config.timer) {
        return new EnergyNotifyTimerLock();
    }
    else if (config.energy && config.notify) {
        return new EnergyNotifyLock();
    }
    else if (config.energy && config.timer) {
        return new EnergyTimerLock();
    }
    else if (config.notify && config.timer) {
        return new NotifyTimerLock();
    }
    else if (config.energy) {
        return new EnergyLock();
    }
    else if (config.notify) {
        return new NotifyLock();
    }
    else if (config.timer) {
        return new TimerLock();
    }
    else {
        return new Lock();
    }
}

// ==================== ВСПОМОГАТЕЛЬНЫЕ ФУНКЦИИ ====================
std::string readFile(const std::string& path) {
    std::ifstream file(path);
    std::stringstream buffer;
    buffer << file.rdbuf();
    return buffer.str();
}

// Получение статуса устройства
std::string getDeviceStatus(IDevice* device, const DeviceConfig& config, const std::string& type) {
    std::string json = "{";
    json += "\"type\": \"" + type + "\",";

    if (device) {
        json += "\"status\": \"" + device->GetStatus() + "\"";

        if (config.energy) {
            json += ", \"energy\": \"" + device->GetEnergyUsage() + "\"";
        }
        if (config.notify) {
            json += ", \"notify\": \"" + device->GetNotifyCount() + "\"";
        }
        if (config.timer) {
            json += ", \"schedule\": \"" + device->GetSchedule() + "\"";
        }
    } else {
        json += "\"status\": \"Не настроено\"";
    }

    json += "}";
    return json;
}

// ==================== ПРИМЕНЕНИЕ КОНФИГУРАЦИИ ====================
void applyConfig(const std::string& device, const DeviceConfig& config) {
    std::lock_guard<std::mutex> guard(deviceMutex);

    if (device == "light") {
        delete light;
        lightConfig = config;
        light = createLight(config);
        std::cout << "[CONFIG] Light: Energy=" << config.energy
                  << ", Notify=" << config.notify
                  << ", Timer=" << config.timer << std::endl;
    }
    else if (device == "thermostat") {
        delete thermo;
        thermoConfig = config;
        thermo = createThermostat(config);
        std::cout << "[CONFIG] Thermostat: Energy=" << config.energy
                  << ", Notify=" << config.notify
                  << ", Timer=" << config.timer << std::endl;
    }
    else if (device == "lock") {
        delete lockDevice;
        lockConfig = config;
        lockDevice = createLock(config);
        std::cout << "[CONFIG] Lock: Energy=" << config.energy
                  << ", Notify=" << config.notify
                  << ", Timer=" << config.timer << std::endl;
    }
}

// ==================== ПРИМЕНЕНИЕ РАСПИСАНИЯ ====================
void applySchedule(const std::string& device, const std::string& start, const std::string& end) {
    std::lock_guard<std::mutex> guard(deviceMutex);

    if (device == "light") {
        if (auto* timerLight = dynamic_cast<EnergyNotifyTimerLight*>(light)) {
            timerLight->SetSchedule(start, end);
        } else if (auto* timerLight = dynamic_cast<EnergyTimerLight*>(light)) {
            timerLight->SetSchedule(start, end);
        } else if (auto* timerLight = dynamic_cast<NotifyTimerLight*>(light)) {
            timerLight->SetSchedule(start, end);
        } else if (auto* timerLight = dynamic_cast<TimerLight*>(light)) {
            timerLight->SetSchedule(start, end);
        }
        std::cout << "[SCHEDULE] Light: " << start << " — " << end << std::endl;
    }
    else if (device == "thermostat") {
        if (auto* timerThermo = dynamic_cast<EnergyNotifyTimerThermostat*>(thermo)) {
            timerThermo->SetSchedule(start, end);
        } else if (auto* timerThermo = dynamic_cast<EnergyTimerThermostat*>(thermo)) {
            timerThermo->SetSchedule(start, end);
        } else if (auto* timerThermo = dynamic_cast<NotifyTimerThermostat*>(thermo)) {
            timerThermo->SetSchedule(start, end);
        } else if (auto* timerThermo = dynamic_cast<TimerThermostat*>(thermo)) {
            timerThermo->SetSchedule(start, end);
        }
        std::cout << "[SCHEDULE] Thermostat: " << start << " — " << end << std::endl;
    }
    else if (device == "lock") {
        if (auto* timerLock = dynamic_cast<EnergyNotifyTimerLock*>(lockDevice)) {
            timerLock->SetSchedule(start, end);
        } else if (auto* timerLock = dynamic_cast<EnergyTimerLock*>(lockDevice)) {
            timerLock->SetSchedule(start, end);
        } else if (auto* timerLock = dynamic_cast<NotifyTimerLock*>(lockDevice)) {
            timerLock->SetSchedule(start, end);
        } else if (auto* timerLock = dynamic_cast<TimerLock*>(lockDevice)) {
            timerLock->SetSchedule(start, end);
        }
        std::cout << "[SCHEDULE] Lock: " << start << " — " << end << std::endl;
    }
}

// ==================== MAIN ====================
int main() {
    // Инициализация с конфигурацией по умолчанию (все функции)
    lightConfig = {false, false, false};
    thermoConfig = {false, false, false};
    lockConfig = {false, false, false};

    light = createLight(lightConfig);
    thermo = createThermostat(thermoConfig);
    lockDevice = createLock(lockConfig);

    std::cout << "=== SmartHome Hub (БЕЗ паттерна Decorator) ===" << std::endl;
    std::cout << "Создано устройств: 3 (из 24 возможных классов)" << std::endl;
    std::cout << "Запуск сервера на http://localhost:8080" << std::endl << std::endl;

    httplib::Server svr;

    // Главная страница
    svr.Get("/", [](const httplib::Request&, httplib::Response& res) {
        res.set_content(readFile("web/index.html"), "text/html");
    });

    // API: Получить текущую конфигурацию
    svr.Get("/api/config", [&](const httplib::Request&, httplib::Response& res) {
        std::string json = R"({
            "light": {"energy": )" + std::to_string(lightConfig.energy) +
            R"(, "notify": )" + std::to_string(lightConfig.notify) +
            R"(, "timer": )" + std::to_string(lightConfig.timer) + R"(},
            "thermo": {"energy": )" + std::to_string(thermoConfig.energy) +
            R"(, "notify": )" + std::to_string(thermoConfig.notify) +
            R"(, "timer": )" + std::to_string(thermoConfig.timer) + R"(},
            "lock": {"energy": )" + std::to_string(lockConfig.energy) +
            R"(, "notify": )" + std::to_string(lockConfig.notify) +
            R"(, "timer": )" + std::to_string(lockConfig.timer) + R"(}
        })";
        res.set_content(json, "application/json");
    });

    // API: Применить конфигурацию
    svr.Post("/api/config/:device", [](const httplib::Request& req, httplib::Response& res) {
        std::string device = req.path_params.at("device");

        DeviceConfig config;
        config.energy = req.body.find("\"energy\":true") != std::string::npos;
        config.notify = req.body.find("\"notify\":true") != std::string::npos;
        config.timer = req.body.find("\"timer\":true") != std::string::npos;

        applyConfig(device, config);

        res.set_content("OK", "text/plain");
    });

    // ⚠️ API: Сохранить расписание
    svr.Post("/api/schedule/:device", [](const httplib::Request& req, httplib::Response& res) {
        std::string device = req.path_params.at("device");

        std::string start = "08:00";
        std::string end = "22:00";

        auto start_pos = req.body.find("\"start\":\"");
        if (start_pos != std::string::npos) {
            start_pos += 9;
            auto end_pos = req.body.find("\"", start_pos);
            if (end_pos != std::string::npos) {
                start = req.body.substr(start_pos, end_pos - start_pos);
            }
        }

        auto end_pos = req.body.find("\"end\":\"");
        if (end_pos != std::string::npos) {
            end_pos += 7;
            auto end_pos2 = req.body.find("\"", end_pos);
            if (end_pos2 != std::string::npos) {
                end = req.body.substr(end_pos, end_pos2 - end_pos);
            }
        }

        applySchedule(device, start, end);

        res.set_content("OK", "text/plain");
    });

    // Управление светом
    svr.Get("/light/on", [](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);
        if (light) { light->Activate(); res.set_content("OK", "text/plain"); }
        else { res.set_content("Error", "text/plain"); }
    });
    svr.Get("/light/off", [](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);
        if (light) { light->Deactivate(); res.set_content("OK", "text/plain"); }
        else { res.set_content("Error", "text/plain"); }
    });

    // Управление термостатом
    svr.Get("/thermostat/on", [](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);
        if (thermo) { thermo->Activate(); res.set_content("OK", "text/plain"); }
        else { res.set_content("Error", "text/plain"); }
    });
    svr.Get("/thermostat/off", [](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);
        if (thermo) { thermo->Deactivate(); res.set_content("OK", "text/plain"); }
        else { res.set_content("Error", "text/plain"); }
    });

    // Управление замком
    svr.Get("/lock/on", [](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);
        if (lockDevice) { lockDevice->Activate(); res.set_content("OK", "text/plain"); }
        else { res.set_content("Error", "text/plain"); }
    });
    svr.Get("/lock/off", [](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);
        if (lockDevice) { lockDevice->Deactivate(); res.set_content("OK", "text/plain"); }
        else { res.set_content("Error", "text/plain"); }
    });

    // API: Получить статус всех устройств
    svr.Get("/status", [&](const httplib::Request&, httplib::Response& res) {
        std::lock_guard<std::mutex> guard(deviceMutex);

        std::string json = "{";
        json += "\"light\": " + getDeviceStatus(light, lightConfig, "light") + ",";
        json += "\"thermo\": " + getDeviceStatus(thermo, thermoConfig, "thermo") + ",";
        json += "\"lock\": " + getDeviceStatus(lockDevice, lockConfig, "lock");
        json += "}";

        res.set_content(json, "application/json");
    });

    std::cout << "Server starting at http://localhost:8080\n";
    std::cout << "Press Ctrl+C to stop\n";
    svr.listen("localhost", 8080);

    // Очистка памяти
    delete light;
    delete thermo;
    delete lockDevice;
    
    return 0;
}