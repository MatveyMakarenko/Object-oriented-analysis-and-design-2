# all_tasks_iter.py
from iterator_base import TaskIterator

class AllTasksIterator(TaskIterator):
    def __init__(self, collection):
        self._collection = collection
        self.index = 0  # Атрибут как на схеме

    def has_next(self) -> bool:
        return self.index < len(self._collection.getTasks())

    def next(self):
        if self.has_next():
            task = self._collection.getTasks()[self.index]
            self.index += 1
            return task
        raise StopIteration

    def has_previous(self) -> bool:
        return self.index > 0

    def previous(self):
        if self.has_previous():
            self.index -= 1
            return self._collection.getTasks()[self.index]
        raise StopIteration

    def reset(self):
        self.index = 0

    def current_index(self) -> int:
        return self.index

    def total(self) -> int:
        return len(self._collection.getTasks())