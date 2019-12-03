import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  isAdmin = localStorage.getItem('isAdmin');
  isExpired = localStorage.getItem('isExpired');
  constructor() { }

}
