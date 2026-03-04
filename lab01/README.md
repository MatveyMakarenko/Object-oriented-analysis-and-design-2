# ОТЧЕТ

Диспетчер Умного Дома (SmartHome Hub)

 ## Проблема:
  
Проблема заключается в том, что создание конфигурации устройств «Умного дома» требует обеспечения строгой совместимости компонентов внутри одной экосистемы. При этом протоколы связи, формат данных и функционал кардинально отличаются в зависимости от производителя (например, премиум-экосистема требует шифрования и расширенной телеметрии, а бюджетная — базового управления). Использование простого условного кода для создания устройств приведет к нарушению принципа открытости/закрытости (OCP), усложнению кода при добавлении новых вендоров и риску несовместимости устройств разных производителей в рамках одной сессии управления.

Пример из кода (без паттерна):

```csharp
//Нарушение OCP: для нового вендора нужно менять этот класс
if (currentVendor == "EcoHome")
{
    ecoLight = new EcoLight();
    ecoThermostat = new EcoThermostat();
}
else if (currentVendor == "TechPro")
{
    techLight = new TechLight();
    techThermostat = new TechThermostat();
}
```

##  Решение:
  Применён паттерн Abstract Factory для создания семейств связанных объектов (устройств одного вендора) без привязки к конкретным классам.
Компоненты решения:
DeviceFactory — абстрактная фабрика, объявляющая методы создания продуктов (CreateLight(), CreateThermostat(), CreateLock()). EcoHomeFactory и TechProFactory — конкретные фабрики для создания устройств бюджетной и премиум экосистем соответственно. Light, Thermostat, Lock — интерфейсы продуктов, определяющие общие методы для всех устройств. Конкретные классы (EcoLight, TechLight и др.) реализуют функционал для конкретного вендора. Клиентом в данной архитектуре выступает MainForm — GUI-форма, которая работает исключительно с интерфейсами фабрик и продуктов, не зная о конкретных классах устройств.

Пример из кода (с паттерном):
```csharp
// Соблюдение OCP: для нового вендора создаётся новый класс фабрики
private void SetFactory(DeviceFactory factory)
{
    _factory = factory;
    _light = factory.CreateLight();
    _thermostat = factory.CreateThermostat();
    _lock = factory.CreateLock();
}

// Вызов из GUI:
SetFactory(new EcoHomeFactory());  // или new TechProFactory()
```

<figure>
<img width="1126" height="754" alt="image" src="https://github.com/user-attachments/assets/0a4fc087-eded-42d2-88f4-e6d1f6c7487c" />
<figcaption style="text-align: center;">Рисунок 1 - Диаграмма классов паттерна Abstract Factory в архитектуре приложения SmartHome Hub</figcaption>
</figure>


<figure>
<img width="833" height="719" alt="image" src="https://github.com/user-attachments/assets/d1e15855-f864-4214-a317-618b2ce21ab3" />
<figcaption>Рисунок 2 - Интерфейс приложения SmartHome Hub (режим EcoHome)</figcaption>
</figure>

<figure>
<img width="812" height="716" alt="image" src="https://github.com/user-attachments/assets/53331b46-3285-48a2-8490-7891505b2308" />
<figcaption>Рисунок 3 - Интерфейс приложения SmartHome Hub (режим TechPro)</figcaption>
</figure>

## Вывод:
Применение паттерна Abstract Factory позволило устранить нарушение принципа Open/Closed, снизить связность между компонентами системы и гарантировать совместимость устройств внутри одной экосистемы. Для добавления нового вендора достаточно создать 4 новых класса (3 продукта + 1 фабрика), не изменяя существующий код клиента (MainForm). Это делает систему расширяемой, поддерживаемой и устойчивой к ошибкам конфигурации.











