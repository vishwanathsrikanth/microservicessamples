import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { IAppState } from '../app.state';
import { Router } from '@angular/router';
import { selectedUser } from './../../app/store/selectors/user.selector';
import { GetUser } from '../store/actions/user.action';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent implements OnInit {

  isExpanded = false;
  user$ = this._store.pipe(select(selectedUser));

 constructor(private _store: Store < IAppState >, private _router: Router){ }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
      this._store.dispatch(new GetUser());
  }
}
