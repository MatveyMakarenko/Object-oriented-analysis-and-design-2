cmake_minimum_required(VERSION 3.10)
project(SmartHomeWithoutPattern)

set(CMAKE_CXX_STANDARD 17)

# ⚠️ СНАЧАЛА создаём исполняемый файл
add_executable(smarthome main.cpp)

# ⚠️ ПОТОМ подключаем библиотеки
target_link_libraries(smarthome ws2_32)

# Копируем web папку в папку сборки
file(COPY ${CMAKE_SOURCE_DIR}/web DESTINATION ${CMAKE_BINARY_DIR})