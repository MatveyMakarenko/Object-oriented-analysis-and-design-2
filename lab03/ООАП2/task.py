# task.py
class Task:
    def __init__(self, title: str, category: str, status: str, priority: str, duration: str):
        self.title = title
        self.category = category
        self.status = status
        self.priority = priority
        self.duration = duration
        self._id = id(self)

    def mark_complete(self):
        self.status = "Готово"

    def __str__(self):
        return f"[{self.priority}] {self.title} ({self.category}) - {self.status}"

    def get_id(self):
        return self._id