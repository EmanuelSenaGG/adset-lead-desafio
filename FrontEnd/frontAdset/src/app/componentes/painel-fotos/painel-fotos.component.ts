import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-painel-fotos',
  templateUrl: './painel-fotos.component.html',
  styleUrls: ['./painel-fotos.component.css']
})
export class PainelFotosComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  fotos: string[] = [
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
      '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
      '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',
    '../../assets/carros/carro.jpg',

  ];
}
