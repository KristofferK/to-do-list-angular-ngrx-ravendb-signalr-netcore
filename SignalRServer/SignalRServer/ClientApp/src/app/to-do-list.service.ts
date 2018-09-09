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
  public taskChanges: ReplaySubject<Task> = new ReplaySubject<Task>();
  public taskDeletes: ReplaySubject<string> = new ReplaySubject<string>();

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

    this.hubConnection.on('TaskCompleteFlagChanged', (task: Task) => {
      this.taskChanges.next(task);
    });

    this.hubConnection.on('TaskDeleted', (id: string) => {
      this.taskDeletes.next(id);
    });

    this.getTasksFromApi();
  }

  private getTasksFromApi(): void {
    this.http.get<Task[]>('https://localhost:44364/api/Tasks/Tasks').subscribe((tasks: Task[]) => {
      tasks.forEach(task => this.tasks.next(task))
    });
  }

  public addTask(task: string) {
    this.hubConnection.invoke('AddTask', task);
  }

  public completeTask(id: string, title: string, completed: boolean) {
    this.hubConnection.invoke('CompleteTask', id, title, completed);
  }

  public deleteTask(id: string) {
    this.hubConnection.invoke('DeleteTask', id);
  }
}
