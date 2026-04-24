# task_collection.py
from abc import ABC, abstractmethod
from iterator_base import TaskIterator

class TaskCollection(ABC):
    """
    <<interface>> TaskCollection
    Определяет контракт для коллекций задач.
    """
    
    @abstractmethod
    def createIterator(self, mode: str) -> TaskIterator:
        """
        Фабричный метод для создания итератора.
        :param mode: режим итерации ("all", "active", "priority", "category")
        :return: экземпляр TaskIterator
        """
        pass