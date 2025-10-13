import { Component, OnInit } from '@angular/core';
import { VeiculoService } from '../../services/veiculo-service.service';
import { InformacoesVeiculosDto } from '../../interfaces/Veiculo/InformacoesVeiculosDto';
import { SwalHandler } from 'src/app/utils/SwalHandler';

@Component({
  selector: 'app-painel-estatisticas',
  templateUrl: './painel-estatisticas.component.html',
  styleUrls: ['./painel-estatisticas.component.css']
})
export class PainelEstatisticasComponent implements OnInit {

    constructor(private _service: VeiculoService) { }
  
    informacoesVeiculos: InformacoesVeiculosDto = {
    ids: [],
    total: 0,
    totalFotos: 0,
    totalSemFotos: 0
  };

    ngOnInit(): void {
      this.ObterInformacoesVeiculos();
    }
  
    ObterInformacoesVeiculos(): void {
      this._service.ObterInformacoesVeiculos().subscribe({
        next: (dados) => {
          this.informacoesVeiculos = dados;
  
        },
        error: (err) => {
          SwalHandler.showFalha('Falha', 'Não foi possivel carregar o total de veiculos e suas fotos ');
        }
      });
    }
}
