import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../../../services/loader.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  loaderShown: boolean = false;

  constructor(private readonly _loaderService: LoaderService) { }

  ngOnInit(): void {
    this._loaderService.loader$
      .subscribe(value => this.loaderShown = value);
  }
}
