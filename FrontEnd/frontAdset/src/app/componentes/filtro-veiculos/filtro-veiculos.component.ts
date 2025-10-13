import { Component, OnInit } from '@angular/core';
import { VeiculoService } from '../../services/veiculo-service.service';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { VeiculoDto } from 'src/app/interfaces/Veiculo/VeiculoDto';

@Component({
  selector: 'app-filtro-veiculos',
  templateUrl: './filtro-veiculos.component.html',
  styleUrls: ['./filtro-veiculos.component.css']
})
export class FiltroVeiculosComponent implements OnInit {

  constructor(private _service: VeiculoService) { }

  veiculos: VeiculoDto[] = []


  ngOnInit(): void {
    this.listarVeiculos();
  }

  listarVeiculos(): void {
    this._service.listarVeiculos().subscribe({
      next: (dados) => {
        this.veiculos = dados;
      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar os veiculos'
        );
      }
    });
  }

}
