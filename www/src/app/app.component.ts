import { Component, OnInit } from '@angular/core';
import { AuthInterceptor } from './services/AuthInterceptor';
import { Auth } from './usecase/entitie/Auth';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  
  constructor(private auth: Auth) {
    
    
  }
  
  ngOnInit(){
      this.auth.get().subscribe({
        next: () => console.log('Auth'),
        error: err => console.error('Auth error', err)
      });
  }
}
