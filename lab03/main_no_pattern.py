# main_modern.py
import customtkinter as ctk
from tkinter import messagebox
from collection_no_pattern import TaskManager
from task import Task
import json

# Настройки темы
ctk.set_appearance_mode("light")
ctk.set_default_color_theme("blue")

class ModernTaskGUI:
    def __init__(self, manager: TaskManager):
        self._manager = manager
        self._current_filter = "all"
        self._current_category = "Все"
        self._task_widgets = []
        
        # Динамический список категорий
        self._categories = ["Работа", "Дом", "Личное", "Учёба", "Покупки"]
        
        self.root = ctk.CTk()
        self.root.title("📋 TaskFlow — Менеджер Задач")
        self.root.geometry("1100x900")
        self.root.minsize(900, 700)
        
        self._create_widgets()
        self._refresh_task_list()
    
    def _create_widgets(self):
        # Главный контейнер
        main_frame = ctk.CTkFrame(self.root, corner_radius=0, fg_color="transparent")
        main_frame.pack(fill="both", expand=True, padx=20, pady=20)
        
        # === Заголовок ===
        header_frame = ctk.CTkFrame(main_frame, height=60, corner_radius=10)
        header_frame.pack(fill="x", pady=(0, 20))
        header_frame.pack_propagate(False)
        
        title_label = ctk.CTkLabel(header_frame, 
                                  text="📋 TaskFlow — Менеджер Задач",
                                  font=ctk.CTkFont(size=21, weight="bold"))
        title_label.pack(side="left", padx=20, pady=20)
        
        # === Панель добавления задачи ===
        add_frame = ctk.CTkFrame(main_frame, corner_radius=10)
        add_frame.pack(fill="x", pady=(0, 20))
        
        ctk.CTkLabel(add_frame, 
                    text="➕ Добавить новую задачу",
                    font=ctk.CTkFont(size=16, weight="bold")).pack(padx=20, pady=(15, 10), anchor="w")
        
        # Поля ввода
        input_frame = ctk.CTkFrame(add_frame, fg_color="transparent")
        input_frame.pack(fill="x", padx=20, pady=10)
        
        ctk.CTkLabel(input_frame, text="Название:").grid(row=0, column=0, padx=(0, 10), sticky="w")
        self.title_entry = ctk.CTkEntry(input_frame, width=350, height=32)
        self.title_entry.grid(row=0, column=1, padx=(0, 15), sticky="ew")
        
        # Категория с кнопкой добавления
        ctk.CTkLabel(input_frame, text="Категория:").grid(row=0, column=2, padx=(0, 10))
        self.category_combo = ctk.CTkComboBox(input_frame, 
                                             values=self._categories,
                                             width=140,
                                             height=32,
                                             state="readonly")
        self.category_combo.set("Работа")
        self.category_combo.grid(row=0, column=3, padx=(0, 5))
        
        # Кнопка добавления категории
        add_category_btn = ctk.CTkButton(input_frame,
                                        text="+",
                                        width=35,
                                        height=32,
                                        font=ctk.CTkFont(size=16, weight="bold"),
                                        fg_color="#2196F3",
                                        hover_color="#1976D2",
                                        command=self._add_new_category)
        add_category_btn.grid(row=0, column=4, padx=(0, 15))
        
        ctk.CTkLabel(input_frame, text="Приоритет:").grid(row=0, column=5, padx=(0, 10))
        self.priority_combo = ctk.CTkComboBox(input_frame,
                                             values=["Высокий", "Средний", "Низкий"],
                                             width=110,
                                             height=32,
                                             state="readonly")
        self.priority_combo.set("Средний")
        self.priority_combo.grid(row=0, column=6)
        
        input_frame.columnconfigure(1, weight=1)
        
        # Кнопка добавления задачи (по центру)
        add_btn = ctk.CTkButton(add_frame,
                               text="➕ Добавить",
                               height=38,
                               width=180,
                               font=ctk.CTkFont(size=13, weight="bold"),
                               fg_color="#4CAF50",
                               hover_color="#45a049",
                               command=self._add_task)
        add_btn.pack(pady=15, anchor="center")
        
        # === Панель фильтров ===
        filter_frame = ctk.CTkFrame(main_frame, corner_radius=10)
        filter_frame.pack(fill="x", pady=(0, 20))
        
        ctk.CTkLabel(filter_frame,
                    text="🔍 Режим просмотра",
                    font=ctk.CTkFont(size=16, weight="bold")).pack(padx=20, pady=(15, 10), anchor="w")
        
        self.filter_var = ctk.StringVar(value="all")
        
        filter_buttons = ctk.CTkFrame(filter_frame, fg_color="transparent")
        filter_buttons.pack(fill="x", padx=20, pady=10)
        
        ctk.CTkRadioButton(filter_buttons, text="Все задачи", 
                          variable=self.filter_var, value="all",
                          command=self._switch_filter).pack(side="left", padx=10)
        ctk.CTkRadioButton(filter_buttons, text="Только активные",
                          variable=self.filter_var, value="active",
                          command=self._switch_filter).pack(side="left", padx=10)
        ctk.CTkRadioButton(filter_buttons, text="По приоритету",
                          variable=self.filter_var, value="priority",
                          command=self._switch_filter).pack(side="left", padx=10)
        ctk.CTkRadioButton(filter_buttons, text="По категории",
                          variable=self.filter_var, value="category",
                          command=self._switch_filter).pack(side="left", padx=10)
        
        # ComboBox для фильтрации по категории
        self.category_filter_combo = ctk.CTkComboBox(filter_buttons,
                                                    values=["Все"] + self._categories,
                                                    width=140,
                                                    height=30,
                                                    state="disabled")
        self.category_filter_combo.set("Все")
        self.category_filter_combo.pack(side="left", padx=10)
        self.category_filter_combo.bind("<<ComboboxSelected>>", self._on_category_selected)
        
        # === Список задач ===
        list_frame = ctk.CTkFrame(main_frame, corner_radius=10)
        list_frame.pack(fill="both", expand=True, pady=(0, 10))
        
        ctk.CTkLabel(list_frame,
                    text="📋 Список задач (двойной клик — выполнить, ✕ — удалить)",
                    font=ctk.CTkFont(size=16, weight="bold")).pack(padx=20, pady=(15, 10), anchor="w")
        
        # Scrollable Frame для задач (БЕЗ height=350!)
        self.scrollable_frame = ctk.CTkScrollableFrame(list_frame)
        self.scrollable_frame.pack(fill="both", expand=True, padx=20, pady=10)
        
        # === КНОПКИ УПРАВЛЕНИЯ (ВИДИМЫЕ!) ===
        btn_frame = ctk.CTkFrame(main_frame, 
                                 fg_color="#f0f0f0",
                                 corner_radius=10,
                                 border_width=2,
                                 border_color="#cccccc")
        btn_frame.pack(fill="x", padx=20, pady=(0, 10))
        
        btn_container = ctk.CTkFrame(btn_frame, fg_color="transparent")
        btn_container.pack(pady=15)
        

        
        ctk.CTkButton(btn_container, text="💾 Экспорт JSON",
                     command=self._export_json,
                     width=200,
                     height=50,
                     font=ctk.CTkFont(size=14, weight="bold"),
                     fg_color="#2196F3",
                     hover_color="#1976D2").pack(side="left", padx=20)
        
        # === Статус бар ===
        status_frame = ctk.CTkFrame(main_frame, corner_radius=8, fg_color="#E3F2FD")
        status_frame.pack(fill="x", padx=20, pady=(0, 10))
        
        self.status_label = ctk.CTkLabel(status_frame,
                                        text="",
                                        font=ctk.CTkFont(size=12, weight="bold"),
                                        text_color="#1565C0")
        self.status_label.pack(padx=10, pady=10)
    
    # Метод добавления новой категории
    def _add_new_category(self):
        dialog = ctk.CTkToplevel(self.root)
        dialog.title("➕ Новая категория")
        dialog.geometry("400x180")
        dialog.transient(self.root)
        dialog.grab_set()
        
        ctk.CTkLabel(dialog, 
                    text="Введите название новой категории:",
                    font=ctk.CTkFont(size=14)).pack(padx=20, pady=(25, 15))
        
        category_entry = ctk.CTkEntry(dialog, width=300, height=35)
        category_entry.pack(padx=20, pady=10)
        category_entry.focus()
        
        def save_category():
            new_category = category_entry.get().strip()
            
            if not new_category:
                messagebox.showwarning("⚠️ Предупреждение", "Введите название категории!", parent=dialog)
                return
            
            if new_category in self._categories:
                messagebox.showwarning("⚠️ Предупреждение", "Такая категория уже существует!", parent=dialog)
                return
            
            self._categories.append(new_category)
            self.category_combo.configure(values=self._categories)
            self.category_filter_combo.configure(values=["Все"] + self._categories)
            self.category_combo.set(new_category)
            
            messagebox.showinfo("✅ Успех", f"Категория '{new_category}' добавлена!", parent=dialog)
            dialog.destroy()
        
        ctk.CTkButton(dialog, 
                     text="💾 Сохранить",
                     command=save_category,
                     width=200,
                     height=40,
                     font=ctk.CTkFont(size=13, weight="bold"),
                     fg_color="#4CAF50",
                     hover_color="#45a049").pack(pady=10)
        
        dialog.bind('<Return>', lambda e: save_category())
    
    def _add_task(self):
        title = self.title_entry.get().strip()
        if not title:
            messagebox.showwarning("⚠️ Предупреждение", "Введите название задачи!", parent=self.root)
            return
        
        task = Task(title=title,
                   category=self.category_combo.get(),
                   priority=self.priority_combo.get())
        self._manager.add_task(task)
        self.title_entry.delete(0, "end")
        self._refresh_task_list()
        messagebox.showinfo("✅ Успех", "Задача добавлена!")
    
    def _switch_filter(self):
        self._current_filter = self.filter_var.get()
        
        if self._current_filter == "category":
            self.category_filter_combo.configure(state="readonly")
            self._current_category = self.category_filter_combo.get()
            self._refresh_task_list()
        else:
            self.category_filter_combo.configure(state="disabled")
            self._current_category = "Все"
            self._refresh_task_list()
    
    def _on_category_selected(self, event=None):
        if self._current_filter == "category":
            self._current_category = self.category_filter_combo.get()
            self._refresh_task_list()
    
    def _get_filtered_tasks(self):
        if self._current_filter == "all":
            return self._manager.get_all_tasks()
        elif self._current_filter == "active":
            return self._manager.get_active_tasks()
        elif self._current_filter == "priority":
            return self._manager.get_priority_sorted_tasks()
        elif self._current_filter == "category":
            if self._current_category == "Все":
                return self._manager.get_all_tasks()
            return self._manager.get_category_tasks(self._current_category)
        return self._manager.get_all_tasks()
    
    def _clear_task_list(self):
        for widget in self.scrollable_frame.winfo_children():
            widget.destroy()
        self._task_widgets = []
    
    def _create_task_row(self, task, index):
        row_frame = ctk.CTkFrame(self.scrollable_frame, corner_radius=8, fg_color="transparent")
        row_frame.pack(fill="x", pady=3, padx=5)
        
        status_icon = "✅" if task.status == "Готово" else "⬜"
        priority_color = {"Высокий": "🔴", "Средний": "🟡", "Низкий": "🟢"}.get(task.priority, "⚪")
        
        task_text = f"{status_icon} {priority_color} [{task.priority}] {task.title} ({task.category}) — {task.status}"
        task_label = ctk.CTkLabel(row_frame, 
                                 text=task_text,
                                 font=ctk.CTkFont(size=12),
                                 anchor="w")
        task_label.pack(side="left", fill="both", expand=True, padx=(10, 5), pady=8)
        
        delete_btn = ctk.CTkButton(row_frame,
                                  text="✕",
                                  width=35,
                                  height=35,
                                  font=ctk.CTkFont(size=16, weight="bold"),
                                  fg_color="#f44336",
                                  hover_color="#da190b",
                                  command=lambda t=task: self._delete_single_task(t))
        delete_btn.pack(side="right", padx=5, pady=5)
        
        row_frame.bind("<Double-Button-1>", lambda e, t=task: self._on_task_double_click(t))
        task_label.bind("<Double-Button-1>", lambda e, t=task: self._on_task_double_click(t))
        
        self._task_widgets.append(row_frame)
    
    def _refresh_task_list(self):
        self._clear_task_list()
        tasks = self._get_filtered_tasks()
        
        for i, task in enumerate(tasks):
            self._create_task_row(task, i)
        
        total = self._manager.get_task_count()
        active = self._manager.get_active_count()
        completed = self._manager.get_completed_count()
        
        filter_names = {
            "all": "Все задачи",
            "active": "Активные",
            "priority": "По приоритету",
            "category": f"Категория: {self._current_category}"
        }
        
        self.status_label.configure(
            text=f"📊 Всего: {total} | Активных: {active} | Выполнено: {completed} | "
                 f"Показано: {len(tasks)} | Режим: {filter_names.get(self._current_filter, 'Все')}"
        )
    
    def _on_task_double_click(self, task):
        if task.status == "Готово":
            task.status = "Активно"
        else:
            task.mark_complete()
        self._refresh_task_list()
    
    def _delete_single_task(self, task):
        if messagebox.askyesno("🗑️ Подтверждение", f"Удалить задачу '{task.title}'?"):
            tasks = self._manager.get_tasks()
            for i, t in enumerate(tasks):
                if t == task:
                    self._manager.remove_task(i)
                    break
            self._refresh_task_list()
    
    def _show_stats(self):
        total = self._manager.get_task_count()
        active = self._manager.get_active_count()
        completed = self._manager.get_completed_count()
        
        stats_text = f"""
📊 СТАТИСТИКА ЗАДАЧ

Всего задач: {total}
Активных: {active}
Выполнено: {completed}

Процент выполнения: {round(completed/total*100, 1) if total > 0 else 0}%
"""
        
        messagebox.showinfo("Статистика", stats_text)
    
    def _export_json(self):
        tasks_data = [{"title": t.title, "category": t.category, "priority": t.priority,
                      "status": t.status, "duration": t.duration} for t in self._manager.get_tasks()]
        
        with open("tasks_export.json", "w", encoding="utf-8") as f:
            json.dump(tasks_data, f, ensure_ascii=False, indent=2)
        
        messagebox.showinfo("✅ Успех", "Экспорт выполнен в tasks_export.json!")
    
    def run(self):
        self.root.mainloop()

def main():
    manager = TaskManager("Мои задачи")
    manager.add_task(Task("Сделать отчёт", "Работа", "Высокий"))
    manager.add_task(Task("Купить продукты", "Дом", "Средний"))
    manager.add_task(Task("Позвонить клиенту", "Работа", "Высокий"))
    manager.add_task(Task("Прочитать книгу", "Личное", "Низкий"))
    manager.add_task(Task("Подготовить презентацию", "Работа", "Высокий"))
    manager.add_task(Task("Убрать квартиру", "Дом", "Средний"))
    
    gui = ModernTaskGUI(manager)
    gui.run()

if __name__ == "__main__":
    main()