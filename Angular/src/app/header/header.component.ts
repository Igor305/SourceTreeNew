import { Component, DoCheck} from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements DoCheck {
  isAdmin = localStorage.getItem('isAdmin');
  isExpired = localStorage.getItem('isExpired');
  Email = localStorage.getItem('Email');
  constructor() { }

  private async singOut(){
    this.isExpired = 'false';
    localStorage.clear();
  }
  
  ngDoCheck(){
    this.isAdmin = localStorage.getItem('isAdmin');
    this.isExpired = localStorage.getItem('isExpired');
    this.Email = localStorage.getItem('Email');
  }
}
