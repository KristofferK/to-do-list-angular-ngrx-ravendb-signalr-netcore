import { Component, OnInit } from '@angular/core';
import { ToDoListService } from '../to-do-list.service';
import { Task } from '../models/task';

@Component({
  selector: 'to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent implements OnInit {
  public tasks: Task[] = [];
  constructor(private toDoListService: ToDoListService) { }

  ngOnInit() {
    this.toDoListService.tasks.subscribe(task => {
      console.log('Received task from replay subject', task);
      this.tasks.push(task);
    });

    this.toDoListService.taskChanges.subscribe(task => {
      console.log('Must update', task);
      const index = this.tasks.findIndex(e => e.id === task.id);
      if (index != -1) {
        this.tasks[index] = task;
      }
    });

    this.toDoListService.taskDeletes.subscribe(id => {
      console.log('Must delete', id);
      const index = this.tasks.findIndex(e => e.id === id);
      console.log('Index is', index);
      if (index != -1) {
        this.tasks.splice(index, 1);
      }
    })
  }

  public addTask(title: string) {
    this.toDoListService.addTask(title);
  }

  public completeTask(id: string, title: string, completed: boolean) {
    this.toDoListService.completeTask(id, title, completed);
  }

  public deleteTask(id: string) {
    this.toDoListService.deleteTask(id);
  }
}
