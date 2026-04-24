# task_manager.py
from typing import List
from task import Task
from task_collection import TaskCollection
from iterator_base import TaskIterator
from all_tasks_iter import AllTasksIterator
from active_tasks_iter import ActiveTasksIterator
from priority_iter import PriorityTasksIterator
from category_iter import CategoryTasksIterator

class TaskManager(TaskCollection):
    """
    Конкретная реализация TaskCollection.
    Управляет коллекцией задач и создаёт итераторы.
    """
    
    def __init__(self, name: str = "Мои задачи"):
        self.name = name
        self.tasks: List[Task] = []
        self._filter_category = "Все"  # Для фильтрации по категории

    def createIterator(self, mode: str) -> TaskIterator:
        """Создаёт итератор в зависимости от режима"""
        if mode == "all":
            return AllTasksIterator(self)
        elif mode == "active":
            return ActiveTasksIterator(self)
        elif mode == "priority":
            return PriorityTasksIterator(self)
        elif mode == "category":
            return CategoryTasksIterator(self, self._filter_category)
        return AllTasksIterator(self)

    def set_filter_category(self, category: str):
        """Устанавливает категорию для фильтрации"""
        self._filter_category = category

    def addTask(self, task: Task):
        self.tasks.append(task)

    def removeTask(self, index: int):
        if 0 <= index < len(self.tasks):
            del self.tasks[index]

    def getTasks(self) -> List[Task]:
        return self.tasks

    def get_task_count(self) -> int:
        return len(self.tasks)

    def get_active_count(self) -> int:
        return len([t for t in self.tasks if t.status != "Готово"])

    def get_completed_count(self) -> int:
        return len([t for t in self.tasks if t.status == "Готово"])