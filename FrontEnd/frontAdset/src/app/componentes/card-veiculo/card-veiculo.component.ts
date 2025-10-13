import { Component, Input, OnInit } from '@angular/core';
import { VeiculoService } from '../../services/veiculo-service.service';
import { VeiculoDto } from '../../interfaces/Veiculo/VeiculoDto';
import { OpcionalVeiculoDto } from '../../interfaces/Veiculo/OpcionalVeiculoDto';
import { SwalHandler } from '../../utils/SwalHandler';
@Component({
  selector: 'app-card-veiculo',
  templateUrl: './card-veiculo.component.html',
  styleUrls: ['./card-veiculo.component.css']
})
export class CardVeiculoComponent implements OnInit {

  @Input() veiculo!: VeiculoDto;

  textoOpcionais! : string;

  constructor(private _service: VeiculoService) { }

  ngOnInit(): void {
    
  }
ngOnChanges(): void {
  if (this.veiculo) {
    this.textoOpcionais = this.formatarOpcionais(this.veiculo.opcionais);
  }
}



// obterDadosCardVeiculo(): void {
//   this._service.ObterDadosVeiculoCard(this.veiculoId).subscribe({
//     next: (dados) => {
//       this.veiculo = dados;
//       this.textoOpcionais = this.formatarOpcionais(dados.opcionais);
//     },
//     error: (err) => {
//       SwalHandler.showFalha(
//         'Erro',
//         err?.message || 'Não foi possível carregar os dados do veículo.'
//       );
//     }
//   });
// }

formatarOpcionais(opcionais: OpcionalVeiculoDto[] | null | undefined): string {
  if (opcionais && opcionais.length > 0) {
    return opcionais.map(op => op.descricao).join('\n');
  }
  return 'Nenhum opcional';
}
}
