# main.py
import customtkinter as ctk
from tkinter import messagebox
from task_manager import TaskManager
from task import Task
from iterator_base import TaskIterator
import json

ctk.set_appearance_mode("light")
ctk.set_default_color_theme("blue")

class TaskPatternGUI:
    def __init__(self, manager: TaskManager):
        self._manager = manager
        self._current_iterator: TaskIterator = None
        self._current_mode = "all"
        self._current_category = "Все"
        self._task_widgets = []
        self._categories = ["Работа", "Дом", "Личное", "Учёба", "Покупки"]
        
        self.root = ctk.CTk()
        self.root.title("📋 TaskFlow — Менеджер Задач")
        self.root.geometry("1100x900")
        self.root.minsize(900, 700)
        
        self._create_widgets()
        self._refresh_task_list()
    
    def _create_widgets(self):
        main_frame = ctk.CTkFrame(self.root, corner_radius=0, fg_color="transparent")
        main_frame.pack(fill="both", expand=True, padx=20, pady=20)
        
        # Заголовок
        header = ctk.CTkFrame(main_frame, height=60, corner_radius=10)
        header.pack(fill="x", pady=(0, 20))
        header.pack_propagate(False)
        ctk.CTkLabel(header, text="📋 TaskFlow — Менеджер Задач",
                    font=ctk.CTkFont(size=21, weight="bold")).pack(side="left", padx=20, pady=20)
        
        # Добавление задачи
        add_frame = ctk.CTkFrame(main_frame, corner_radius=10)
        add_frame.pack(fill="x", pady=(0, 20))
        ctk.CTkLabel(add_frame, text="➕ Добавить новую задачу", font=ctk.CTkFont(size=16, weight="bold")).pack(padx=20, pady=(15, 10), anchor="w")
        
        input_frame = ctk.CTkFrame(add_frame, fg_color="transparent")
        input_frame.pack(fill="x", padx=20, pady=10)
        ctk.CTkLabel(input_frame, text="Название:").grid(row=0, column=0, padx=(0, 10), sticky="w")
        self.title_entry = ctk.CTkEntry(input_frame, width=350, height=32)
        self.title_entry.grid(row=0, column=1, padx=(0, 15), sticky="ew")
        
        ctk.CTkLabel(input_frame, text="Категория:").grid(row=0, column=2, padx=(0, 10))
        self.category_combo = ctk.CTkComboBox(input_frame, values=self._categories, width=140, height=32, state="readonly")
        self.category_combo.set("Работа")
        self.category_combo.grid(row=0, column=3, padx=(0, 5))
        
        ctk.CTkButton(input_frame, text="+", width=35, height=32, font=ctk.CTkFont(size=16, weight="bold"),
                     fg_color="#2196F3", hover_color="#1976D2", command=self._add_category).grid(row=0, column=4, padx=(0, 15))
        
        ctk.CTkLabel(input_frame, text="Приоритет:").grid(row=0, column=5, padx=(0, 10))
        self.priority_combo = ctk.CTkComboBox(input_frame, values=["Высокий", "Средний", "Низкий"], width=110, height=32, state="readonly")
        self.priority_combo.set("Средний")
        self.priority_combo.grid(row=0, column=6)
        input_frame.columnconfigure(1, weight=1)
        
        ctk.CTkButton(add_frame, text="➕ Добавить", height=38, width=180, font=ctk.CTkFont(size=13, weight="bold"),
                     fg_color="#4CAF50", hover_color="#45a049", command=self._add_task).pack(pady=15, anchor="center")
        
        # Фильтры (Итераторы)
        filter_frame = ctk.CTkFrame(main_frame, corner_radius=10)
        filter_frame.pack(fill="x", pady=(0, 20))
        ctk.CTkLabel(filter_frame, text="🔍 Режим просмотра", font=ctk.CTkFont(size=16, weight="bold")).pack(padx=20, pady=(15, 10), anchor="w")
        
        self.mode_var = ctk.StringVar(value="all")
        btns = ctk.CTkFrame(filter_frame, fg_color="transparent")
        btns.pack(fill="x", padx=20, pady=10)
        
        ctk.CTkRadioButton(btns, text="Все задачи", variable=self.mode_var, value="all", command=self._switch_iterator).pack(side="left", padx=10)
        ctk.CTkRadioButton(btns, text="Только активные", variable=self.mode_var, value="active", command=self._switch_iterator).pack(side="left", padx=10)
        ctk.CTkRadioButton(btns, text="По приоритету", variable=self.mode_var, value="priority", command=self._switch_iterator).pack(side="left", padx=10)
        ctk.CTkRadioButton(btns, text="По категории", variable=self.mode_var, value="category", command=self._switch_iterator).pack(side="left", padx=10)
        
        self.cat_filter = ctk.CTkComboBox(btns, values=["Все"] + self._categories, width=140, height=30, state="disabled",
                                         command=lambda val: self._on_cat_select(val))
        self.cat_filter.set("Все")
        self.cat_filter.pack(side="left", padx=10)
        
        # Список задач
        list_frame = ctk.CTkFrame(main_frame, corner_radius=10)
        list_frame.pack(fill="both", expand=True, pady=(0, 10))
        ctk.CTkLabel(list_frame, text="📋 Список задач (двойной клик — выполнить, ✕ — удалить)",
                    font=ctk.CTkFont(size=16, weight="bold")).pack(padx=20, pady=(15, 10), anchor="w")
        
        self.scroll_frame = ctk.CTkScrollableFrame(list_frame)
        self.scroll_frame.pack(fill="both", expand=True, padx=20, pady=10)
        
        # Кнопки
        btn_frame = ctk.CTkFrame(main_frame, fg_color="#f0f0f0", corner_radius=10, border_width=2, border_color="#cccccc")
        btn_frame.pack(fill="x", padx=20, pady=(0, 10))
        cont = ctk.CTkFrame(btn_frame, fg_color="transparent")
        cont.pack(pady=15)
        ctk.CTkButton(cont, text="📊 Статистика", command=self._show_stats, width=200, height=50, font=ctk.CTkFont(size=14, weight="bold"),
                     fg_color="#2196F3", hover_color="#1976D2").pack(side="left", padx=20)
        ctk.CTkButton(cont, text="💾 Экспорт JSON", command=self._export_json, width=200, height=50, font=ctk.CTkFont(size=14, weight="bold"),
                     fg_color="#2196F3", hover_color="#1976D2").pack(side="left", padx=20)
        
        # Статус бар
        st_frame = ctk.CTkFrame(main_frame, corner_radius=8, fg_color="#E3F2FD")
        st_frame.pack(fill="x", padx=20, pady=(0, 10))
        self.status_lbl = ctk.CTkLabel(st_frame, text="", font=ctk.CTkFont(size=12, weight="bold"), text_color="#1565C0")
        self.status_lbl.pack(padx=10, pady=10)
    
    def _add_category(self):
        dlg = ctk.CTkToplevel(self.root)
        dlg.title("➕ Новая категория"); dlg.geometry("400x180"); dlg.transient(self.root); dlg.grab_set()
        ctk.CTkLabel(dlg, text="Введите название новой категории:", font=ctk.CTkFont(size=14)).pack(padx=20, pady=(25, 15))
        ent = ctk.CTkEntry(dlg, width=300, height=35); ent.pack(padx=20, pady=10); ent.focus()
        def save():
            val = ent.get().strip()
            if not val: messagebox.showwarning("⚠️", "Введите название!", parent=dlg); return
            if val in self._categories: messagebox.showwarning("⚠️", "Уже существует!", parent=dlg); return
            self._categories.append(val)
            self.category_combo.configure(values=self._categories)
            self.cat_filter.configure(values=["Все"] + self._categories)
            self.category_combo.set(val)
            messagebox.showinfo("✅", f"Категория '{val}' добавлена!", parent=dlg); dlg.destroy()
        ctk.CTkButton(dlg, text="💾 Сохранить", command=save, width=200, height=40, font=ctk.CTkFont(size=13, weight="bold"),
                     fg_color="#4CAF50", hover_color="#45a049").pack(pady=10)
        dlg.bind('<Return>', lambda e: save())
    
    def _add_task(self):
        title = self.title_entry.get().strip()
        if not title: messagebox.showwarning("⚠️", "Введите название!", parent=self.root); return
        self._manager.addTask(Task(title=title, category=self.category_combo.get(), status="Активно", 
                                   priority=self.priority_combo.get(), duration="0"))
        self.title_entry.delete(0, "end")
        self._refresh_task_list()
        messagebox.showinfo("✅", "Задача добавлена!", parent=self.root)
    
    def _switch_iterator(self):
        self._current_mode = self.mode_var.get()
        if self._current_mode == "category":
            self.cat_filter.configure(state="readonly")
            self._current_category = self.cat_filter.get()
            self._manager.set_filter_category(self._current_category)
        else:
            self.cat_filter.configure(state="disabled")
            self._current_category = "Все"
            self._manager.set_filter_category(self._current_category)
        self._refresh_task_list()
    
    def _on_cat_select(self, val):
        if self._current_mode == "category":
            self._current_category = val
            self._manager.set_filter_category(val)
            self._refresh_task_list()
    
    def _refresh_task_list(self):
        for w in self.scroll_frame.winfo_children(): w.destroy()
        self._task_widgets = []
        
        # 🔹 Создаём итератор через фабрику
        self._current_iterator = self._manager.createIterator(self._current_mode)
        
        # 🔹 Перебираем через интерфейс итератора
        while self._current_iterator.has_next():
            task = self._current_iterator.next()
            self._render_task_row(task)
        
        # Обновление статуса
        total = self._manager.get_task_count()
        active = self._manager.get_active_count()
        completed = self._manager.get_completed_count()
        names = {"all": "Все", "active": "Активные", "priority": "По приоритету", "category": f"Категория: {self._current_category}"}
        self.status_lbl.configure(text=f"📊 Всего: {total} | Активных: {active} | Выполнено: {completed} | Показано: {self._current_iterator.total()} | Режим: {names[self._current_mode]}")
    
    def _render_task_row(self, task):
        row = ctk.CTkFrame(self.scroll_frame, corner_radius=8, fg_color="transparent")
        row.pack(fill="x", pady=3, padx=5)
        icon = "✅" if task.status == "Готово" else "⬜"
        p_color = {"Высокий": "🔴", "Средний": "🟡", "Низкий": "🟢"}.get(task.priority, "⚪")
        lbl = ctk.CTkLabel(row, text=f"{icon} {p_color} [{task.priority}] {task.title} ({task.category}) — {task.status}",
                          font=ctk.CTkFont(size=12), anchor="w")
        lbl.pack(side="left", fill="both", expand=True, padx=(10, 5), pady=8)
        btn = ctk.CTkButton(row, text="✕", width=35, height=35, font=ctk.CTkFont(size=16, weight="bold"),
                           fg_color="#f44336", hover_color="#da190b", command=lambda t=task: self._delete_task(t))
        btn.pack(side="right", padx=5, pady=5)
        row.bind("<Double-Button-1>", lambda e, t=task: self._toggle_task(t))
        lbl.bind("<Double-Button-1>", lambda e, t=task: self._toggle_task(t))
        self._task_widgets.append(row)
    
    def _toggle_task(self, task):
        task.mark_complete() if task.status != "Готово" else setattr(task, 'status', 'Активно')
        self._refresh_task_list()
    
    def _delete_task(self, task):
        if messagebox.askyesno("🗑️", f"Удалить '{task.title}'?"):
            tasks = self._manager.getTasks()
            for i, t in enumerate(tasks):
                if t == task: self._manager.removeTask(i); break
            self._refresh_task_list()
    
    def _show_stats(self):
        t, a, c = self._manager.get_task_count(), self._manager.get_active_count(), self._manager.get_completed_count()
        pct = round(c/t*100, 1) if t > 0 else 0
        messagebox.showinfo("📊 Статистика", f"Всего: {t}\nАктивных: {a}\nВыполнено: {c}\nПрогресс: {pct}%")
    
    def _export_json(self):
        data = [{"title": t.title, "category": t.category, "priority": t.priority, "status": t.status} for t in self._manager.getTasks()]
        with open("tasks_pattern_export.json", "w", encoding="utf-8") as f: json.dump(data, f, ensure_ascii=False, indent=2)
        messagebox.showinfo("✅", "Экспорт выполнен!")
    
    def run(self): self.root.mainloop()

def main():
    mgr = TaskManager("Мои задачи")
    mgr.addTask(Task("Сделать отчёт", "Работа", "Активно", "Высокий", "2 часа"))
    mgr.addTask(Task("Купить продукты", "Дом", "Активно", "Средний", "30 мин"))
    mgr.addTask(Task("Позвонить клиенту", "Работа", "Активно", "Высокий", "15 мин"))
    mgr.addTask(Task("Прочитать книгу", "Личное", "Активно", "Низкий", "1 час"))
    
    TaskPatternGUI(mgr).run()

if __name__ == "__main__":
    main()