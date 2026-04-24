# collection_no_pattern.py
from typing import List
from task import Task

class TaskManager:
    """
    ❌ НАРУШЕНИЕ SRP: Класс отвечает и за хранение, и за фильтрацию, и за сортировку.
    ❌ НАРУШЕНИЕ OCP: При добавлении нового режима просмотра нужно менять этот класс.
    """
    
    def __init__(self, name: str = "Мои задачи"):
        self._name = name
        self._tasks: List[Task] = []
    
    def add_task(self, task: Task):
        self._tasks.append(task)
    
    def remove_task(self, index: int):
        if 0 <= index < len(self._tasks):
            del self._tasks[index]
    
    def get_tasks(self) -> List[Task]:
        return self._tasks
    
    def get_all_tasks(self) -> List[Task]:
        return self._tasks
    
    def get_active_tasks(self) -> List[Task]:
        return [t for t in self._tasks if t.status != "Готово"]
    
    def get_priority_sorted_tasks(self) -> List[Task]:
        priority_order = {"Высокий": 0, "Средний": 1, "Низкий": 2}
        return sorted(self._tasks, key=lambda t: priority_order.get(t.priority, 3))
    
    def get_category_tasks(self, category: str) -> List[Task]:
        if category == "Все":
            return self._tasks
        return [t for t in self._tasks if t.category == category]
    
    def get_task_count(self) -> int:
        return len(self._tasks)
    
    def get_active_count(self) -> int:
        return len([t for t in self._tasks if t.status != "Готово"])
    
    def get_completed_count(self) -> int:
        return len([t for t in self._tasks if t.status == "Готово"])