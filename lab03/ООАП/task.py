# task.py
class Task:
    def __init__(self, title: str, category: str, priority: str, 
                 status: str = "Активно", duration: int = 0):
        self.title = title
        self.category = category
        self.priority = priority
        self.status = status
        self.duration = duration
        self._id = id(self)
    
    def mark_complete(self):
        self.status = "Готово"
    
    def __str__(self):
        return f"[{self.priority}] {self.title} ({self.category}) - {self.status}"
    
    def get_id(self):
        return self._id