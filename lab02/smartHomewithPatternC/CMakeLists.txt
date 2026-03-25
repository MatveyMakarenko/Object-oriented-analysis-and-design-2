cmake_minimum_required(VERSION 3.10)
project(SmartHomeWithPattern)

set(CMAKE_CXX_STANDARD 17)

add_executable(smarthome main.cpp)

target_link_libraries(smarthome ws2_32)

file(COPY ${CMAKE_SOURCE_DIR}/web DESTINATION ${CMAKE_BINARY_DIR})