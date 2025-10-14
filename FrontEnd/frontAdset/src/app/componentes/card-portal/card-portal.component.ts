import { Component, Input, OnInit } from '@angular/core';
import { PortalDto } from '../../interfaces/Portal/PortalDto';
import { PacotePortalDto } from 'src/app/interfaces/Pacote/PacotePortalDto';

@Component({
  selector: 'app-card-portal',
  templateUrl: './card-portal.component.html',
  styleUrls: ['./card-portal.component.css']
})
export class CardPortalComponent implements OnInit {

  @Input() portal!: PortalDto;
  @Input() pacotesVeiculoPortal!: PacotePortalDto[];
  pacoteSelecionadoId: number | null = null;

  constructor() { }

  ngOnInit(): void {
    this.atribuirIdSelecionado();
  }

  atribuirIdSelecionado(): void {
    if (!this.pacotesVeiculoPortal || !this.portal) return;

    const pacote = this.pacotesVeiculoPortal.find(
      p => p.portalId === this.portal.Id
    );

    if (pacote) {
      this.pacoteSelecionadoId = pacote.pacoteId;
    }
  }


  selecionarPacote(pacoteId: number, event: Event): void {
  const checkbox = event.target as HTMLInputElement;

  if (checkbox.checked) {
    this.pacoteSelecionadoId = pacoteId; 
  } else {
    this.pacoteSelecionadoId = null; 
  }
}

}
