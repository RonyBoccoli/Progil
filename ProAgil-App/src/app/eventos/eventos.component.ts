import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

eventos: any;
  /*
  eventos: any = [
    {
      EventoId: 1,
      Tema: 'Angular',
      Local: 'Belo Horinte'
    },
    {
      EventoId: 2,
      Tema: '.Net Core',
      Local: 'São Paulo'
    },
    {
      EventoId: 3,
      Tema: 'Angular e .Net Core',
      Local: 'Rio de Janeiro'
    }
  ];
*/

  constructor(private http: HttpClient) { }

  ngOnInit() {
   this.getEventos();
  }

  getEventos(){
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.eventos = response;
      console.log(this.eventos);
    }, error => {
      console.log(error);
    }
    );
  }

}