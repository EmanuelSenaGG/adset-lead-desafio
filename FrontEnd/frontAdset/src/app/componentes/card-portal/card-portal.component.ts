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
  @Input() veiculoId!: number;

  constructor() { }

  ngOnInit(): void {
    this.atribuirIdSelecionado();
  
  }

  private atribuirIdSelecionado(): void {
    if (!this.pacotesVeiculoPortal || !this.portal) return;

    let pacote = this.pacotesVeiculoPortal.find(
      p => p.idPortal == this.portal.id
    );
    if (pacote) {
      this.pacoteSelecionadoId = pacote.pacoteId;
    }
  }


  public selecionarPacote(pacoteId: number, event: Event): void {
  const checkbox = event.target as HTMLInputElement;

  if (checkbox.checked) {
    this.pacoteSelecionadoId = pacoteId; 
  } else {
    this.pacoteSelecionadoId = null; 
  }
}

 public obterDadosParaSalvar(): { veiculoId: number, portalId: number, pacoteId: number | null } {
    return {
      veiculoId: this.veiculoId,
      portalId: this.portal.id,
      pacoteId: this.pacoteSelecionadoId
    };
  }
}
