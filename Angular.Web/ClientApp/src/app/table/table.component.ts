import { Component } from '@angular/core';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {

  constructor() {
    this.entResource = 'products'
    this.title = 'products'
  }

  public entResource: string;
  public title: string;
}
