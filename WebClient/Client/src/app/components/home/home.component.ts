import { Component, OnInit } from '@angular/core';
import { TodoDirectoryService } from '../../services/todo-directory.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private readonly _todoDirectoryService: TodoDirectoryService) { }

  ngOnInit(): void {
    this._todoDirectoryService.getAll()
      .subscribe();
  }
}
