# iterator_base.py
from abc import ABC, abstractmethod

class TaskIterator(ABC):
    @abstractmethod
    def has_next(self) -> bool:
        pass

    @abstractmethod
    def next(self):
        pass

    @abstractmethod
    def has_previous(self) -> bool:
        pass

    @abstractmethod
    def previous(self):
        pass

    @abstractmethod
    def reset(self):
        pass

    @abstractmethod
    def current_index(self) -> int:
        pass

    @abstractmethod
    def total(self) -> int:
        pass