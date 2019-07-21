import { Component } from '@angular/core';
import Modeler from 'dmn-js/lib/Modeler';
import { HttpClient } from '@angular/common/http';
import * as $ from 'jquery';
import { MsalService }  from './services/msal.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'contosospa';
  constructor(private http: HttpClient, private msalService: MsalService) {

    this.http.get('../assets/val.xml', { responseType: 'text' }).subscribe(x => {
      const xml = x; // my DMN 1.1 xml
      const dmnModeler = new Modeler({
        container: '.canvas',
        height: 500,
        width: '100%',
        keyboard: {
          bindTo: window
        }
      });

      dmnModeler.on('element.hover', function(e){
        console.log('It did work');
      });

      dmnModeler.importXML(xml, function (err) {
        console.log('*********************');
        if (err) {
          return console.error('could not import DMN 1.1 diagram', err);
        }
        
        // fetch currently active view
        var activeView = dmnModeler.getActiveView();

        // apply initial logic in DRD view
        if (activeView.type === 'drd') {
          var activeEditor = dmnModeler.getActiveViewer();

          // access active editor components
          var canvas = activeEditor.get('canvas');

          // zoom to fit full viewport
          canvas.zoom('fit-viewport');
        }
      });

    });
  }

  useremail(){
    let useremail = this.msalService.getUserEmail();
    return useremail;
  }

  login(){
    this.msalService.login();
  }

  signup(){
    this.msalService.signup();
  }

  logout(){
    this.msalService.logout();
  }

  isUserLoggedIn(){
    return this.msalService.isLoggedIn();
  }
  
}
