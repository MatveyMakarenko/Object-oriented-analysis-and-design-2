# priority_iter.py
from iterator_base import TaskIterator

class PriorityTasksIterator(TaskIterator):
    def __init__(self, collection):
        self._collection = collection
        self.index = 0
        # Сортировка
        priority_order = {"Высокий": 0, "Средний": 1, "Низкий": 2}
        self._sorted = sorted(collection.getTasks(), key=lambda t: priority_order.get(t.priority, 3))

    def has_next(self) -> bool:
        return self.index < len(self._sorted)

    def next(self):
        if self.has_next():
            task = self._sorted[self.index]
            self.index += 1
            return task
        raise StopIteration

    def has_previous(self) -> bool:
        return self.index > 0

    def previous(self):
        if self.has_previous():
            self.index -= 1
            return self._sorted[self.index]
        raise StopIteration

    def reset(self):
        self.index = 0

    def current_index(self) -> int:
        return self.index

    def total(self) -> int:
        return len(self._sorted)