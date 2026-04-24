# category_iter.py
from iterator_base import TaskIterator

class CategoryTasksIterator(TaskIterator):
    def __init__(self, collection, category: str):
        self._collection = collection
        self.category = category  # Атрибут как на схеме
        self.index = 0
        # Фильтрация по категории
        if category == "Все":
            self._filtered = collection.getTasks()
        else:
            self._filtered = [t for t in collection.getTasks() if t.category == category]

    def has_next(self) -> bool:
        return self.index < len(self._filtered)

    def next(self):
        if self.has_next():
            task = self._filtered[self.index]
            self.index += 1
            return task
        raise StopIteration

    def has_previous(self) -> bool:
        return self.index > 0

    def previous(self):
        if self.has_previous():
            self.index -= 1
            return self._filtered[self.index]
        raise StopIteration

    def reset(self):
        self.index = 0

    def current_index(self) -> int:
        return self.index

    def total(self) -> int:
        return len(self._filtered)