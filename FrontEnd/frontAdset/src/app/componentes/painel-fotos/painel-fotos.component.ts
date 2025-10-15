import { Component, Input, OnInit } from '@angular/core';
import { FotoDto } from 'src/app/interfaces/Foto/FotoDto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-painel-fotos',
  templateUrl: './painel-fotos.component.html',
  styleUrls: ['./painel-fotos.component.css']
})
export class PainelFotosComponent implements OnInit {
  @Input() fotos: FotoDto[] = [];
  apiUrlBase: string = environment.apiBase;
  constructor() { }

  ngOnInit(): void {
  }

}
