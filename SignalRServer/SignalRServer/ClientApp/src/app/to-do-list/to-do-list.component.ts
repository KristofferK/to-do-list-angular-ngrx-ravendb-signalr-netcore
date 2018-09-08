import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent implements OnInit {
  private hubConnection: HubConnection;

  constructor() { }

  ngOnInit() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44364/toDoHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on('TaskAdded', (task: string) => {
      alert('Someone added a task: ' + task)
    });
  }

  public addRandomTask() {
    this.hubConnection.invoke('AddTask', 'My task').then(e => {
      console.log('AddTask completed ' + e);
    })
  }

}
