import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.css']
})
export class AccountPageComponent implements OnInit {

  appTitle = environment.applicationName;
  constructor(private titleService: Title) { }

  ngOnInit() {
    this.titleService.setTitle(`${this.appTitle} - Account`);
  }

}
