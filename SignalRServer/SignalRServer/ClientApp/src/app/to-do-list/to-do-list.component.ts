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
    })
  }

  public addRandomTask() {
    this.toDoListService.addTask('My task');
  }

}
