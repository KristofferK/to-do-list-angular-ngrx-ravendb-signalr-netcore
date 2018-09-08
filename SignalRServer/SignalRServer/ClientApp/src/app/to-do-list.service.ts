import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Task } from './models/task';
import { Observable, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToDoListService {
  private hubConnection: HubConnection;
  public tasks: ReplaySubject<Task> = new ReplaySubject<Task>();

  constructor(private http: HttpClient) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44364/toDoHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on('TaskAdded', (task: Task) => {
      this.tasks.next(task);
    });

    this.getTasksFromApi();
  }

  public addTask(task: string) {
    this.hubConnection.invoke('AddTask', task);
  }

  private getTasksFromApi(): void {
    this.http.get<Task[]>('https://localhost:44364/api/Tasks/Tasks').subscribe((tasks: Task[]) => {
      tasks.forEach(task => this.tasks.next(task))
    });
  }
}
