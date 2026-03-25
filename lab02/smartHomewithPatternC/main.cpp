#include "httplib.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include <mutex>
#include <memory>

// Подключаем все классы
#include "devices/IDevice.h"
#include "devices/Light.h"
#include "devices/Thermostat.h"
#include "devices/Lock.h"
#include "devices/EnergyDecorator.h"
#include "devices/NotifyDecorator.h"
#include "devices/TimerDecorator.h"
struct DeviceConfig {
    bool energy = false;
    bool notify = false;
    bool timer = false;
    std::string scheduleStart = "08:00";
    std::string scheduleEnd = "22:00";
};

// Глобальные устройства
IDevice* light = nullptr;
IDevice* thermo = nullptr;
IDevice* lockDevice = nullptr;

DeviceConfig lightConfig, thermoConfig, lockConfig;
std::mutex deviceMutex;

// Парсинг JSON
std::string parseJsonString(const std::string& json, const std::string& key) {
    std::string searchKey = "\"" + key + "\":\"";
    auto pos = json.find(searchKey);
    if (pos != std::string::npos) {
        pos += searchKey.length();
        auto endPos = json.find("\"", pos);
        if (endPos != std::string::npos) {
            return json.substr(pos, endPos - pos);
        }
    }
    return "";
}

bool parseJsonBool(const std::string& json, const std::string& key) {
    std::string searchKey = "\"" + key + "\":true";
    return json.find(searchKey) != std::string::npos;
}

IDevice* createDevice(const std::string& type, const DeviceConfig& config) {
    IDevice* device = nullptr;

    if (type == "light") {
        device = new Light();
    } else if (type == "thermostat") {
        device = new Thermostat();
    } else if (type == "lock") {
        device = new Lock();
    }

    if (config.energy) {
        device = new EnergyDecorator(device);
    }
    if (config.notify) {
        device = new NotifyDecorator(device);
    }
    if (config.timer) {
        device = new TimerDecorator(device);
    }

    return device;
}

std::string readFile(const std::string& path) {
    std::ifstream file(path);
    std::stringstream buffer;
    buffer << file.rdbuf();
    return buffer.str();
}

std::string getDeviceStatus(IDevice* device, const DeviceConfig& config) {
    std::string json = "{";

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
    }

    json += "}";
    return json;
}

void applyConfig(const std::string& device, const DeviceConfig& config) {
    std::lock_guard<std::mutex> guard(deviceMutex);

    if (device == "light") {
        delete light;
        lightConfig = config;
        light = createDevice("light", config);

        if (config.timer && light) {
            light->SetSchedule(config.scheduleStart, config.scheduleEnd);
        }

        std::cout << "[CONFIG] Light: Energy=" << config.energy
                  << ", Notify=" << config.notify
                  << ", Timer=" << config.timer
                  << ", Schedule=" << config.scheduleStart << "-" << config.scheduleEnd
                  << std::endl;
    }
    else if (device == "thermostat") {  // ✅ ЕДИНОЕ ИМЯ
        delete thermo;
        thermoConfig = config;
        thermo = createDevice("thermostat", config);

        if (config.timer && thermo) {
            thermo->SetSchedule(config.scheduleStart, config.scheduleEnd);
        }

        std::cout << "[CONFIG] Thermostat: Energy=" << config.energy
                  << ", Notify=" << config.notify
                  << ", Timer=" << config.timer
                  << ", Schedule=" << config.scheduleStart << "-" << config.scheduleEnd
                  << std::endl;
    }
    else if (device == "lock") {
        delete lockDevice;
        lockConfig = config;
        lockDevice = createDevice("lock", config);

        if (config.timer && lockDevice) {
            lockDevice->SetSchedule(config.scheduleStart, config.scheduleEnd);
        }

        std::cout << "[CONFIG] Lock: Energy=" << config.energy
                  << ", Notify=" << config.notify
                  << ", Timer=" << config.timer
                  << ", Schedule=" << config.scheduleStart << "-" << config.scheduleEnd
                  << std::endl;
    }
}

int main() {
    lightConfig = {false, false, false, "08:00", "22:00"};
    thermoConfig = {false, false, false, "08:00", "22:00"};
    lockConfig = {false, false, false, "08:00", "22:00"};

    light = createDevice("light", lightConfig);
    thermo = createDevice("thermostat", thermoConfig);
    lockDevice = createDevice("lock", lockConfig);

    std::cout << "SmartHome Hub (С паттерном Decorator)" << std::endl;
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
            R"(, "timer": )" + std::to_string(lightConfig.timer) +
            R"(, "scheduleStart": ")" + lightConfig.scheduleStart +
            R"(", "scheduleEnd": ")" + lightConfig.scheduleEnd + R"("},
            "thermostat": )" +
            R"({"energy": )" + std::to_string(thermoConfig.energy) +
            R"(, "notify": )" + std::to_string(thermoConfig.notify) +
            R"(, "timer": )" + std::to_string(thermoConfig.timer) +
            R"(, "scheduleStart": ")" + thermoConfig.scheduleStart +
            R"(", "scheduleEnd": ")" + thermoConfig.scheduleEnd + R"("},
            "lock": {"energy": )" + std::to_string(lockConfig.energy) +
            R"(, "notify": )" + std::to_string(lockConfig.notify) +
            R"(, "timer": )" + std::to_string(lockConfig.timer) +
            R"(, "scheduleStart": ")" + lockConfig.scheduleStart +
            R"(", "scheduleEnd": ")" + lockConfig.scheduleEnd + R"("}
        })";
        res.set_content(json, "application/json");
    });

    // API: Применить конфигурацию
    svr.Post("/api/config/:device", [](const httplib::Request& req, httplib::Response& res) {
        std::string device = req.path_params.at("device");

        DeviceConfig config;
        config.energy = parseJsonBool(req.body, "energy");
        config.notify = parseJsonBool(req.body, "notify");
        config.timer = parseJsonBool(req.body, "timer");

        config.scheduleStart = parseJsonString(req.body, "scheduleStart");
        config.scheduleEnd = parseJsonString(req.body, "scheduleEnd");

        if (config.scheduleStart.empty()) config.scheduleStart = "08:00";
        if (config.scheduleEnd.empty()) config.scheduleEnd = "22:00";

        applyConfig(device, config);

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
        json += "\"light\": " + getDeviceStatus(light, lightConfig) + ",";
        json += "\"thermostat\": " + getDeviceStatus(thermo, thermoConfig) + ",";
        json += "\"lock\": " + getDeviceStatus(lockDevice, lockConfig);
        json += "}";

        res.set_content(json, "application/json");
    });

    std::cout << "Server starting at http://localhost:8080\n";
    svr.listen("localhost", 8080);

    delete light;
    delete thermo;
    delete lockDevice;
    
    return 0;
}