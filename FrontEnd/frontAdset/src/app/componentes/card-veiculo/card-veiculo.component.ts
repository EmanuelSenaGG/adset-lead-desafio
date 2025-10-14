import { Component, Input, OnInit } from '@angular/core';
import { VeiculoService } from '../../services/veiculo-service.service';
import { VeiculoDto } from '../../interfaces/Veiculo/VeiculoDto';
import { OpcionalVeiculoDto } from '../../interfaces/Veiculo/OpcionalVeiculoDto';
import { Router } from '@angular/router';
import { SwalHandler } from '../../utils/SwalHandler';
import { PortalDto } from 'src/app/interfaces/Portal/PortalDto';

@Component({
  selector: 'app-card-veiculo',
  templateUrl: './card-veiculo.component.html',
  styleUrls: ['./card-veiculo.component.css']
})
export class CardVeiculoComponent implements OnInit {

  @Input() veiculo!: VeiculoDto;
  @Input() portais!: PortalDto[];


  textoOpcionais!: string;

  constructor(private _service: VeiculoService,
    private router: Router
  ) { }

  ngOnInit(): void {
   
  }



  ngOnChanges(): void {
    if (this.veiculo) {
      this.textoOpcionais = this.formatarOpcionais(this.veiculo.opcionais);
    }
  }



  formatarOpcionais(opcionais: OpcionalVeiculoDto[] | null | undefined): string {
    if (opcionais && opcionais.length > 0) {
      return opcionais.map(op => op.descricao).join('\n');
    }
    return 'Nenhum opcional';
  }


  editarVeiculo(id: number): void {
    this.router.navigate(['/veiculo/editar', id]);
  }

  deletarVeiculo(id: number): void {

    this._service.deletarVeiculo(id).subscribe({
      next: () =>
        SwalHandler.showSucessoRedirecionamento(
          this.router,
          'Sucesso',
          'Veículo deletado com sucesso!',
          '/veiculos'
        ),
      error: () =>
        SwalHandler.showFalha('Falha', 'Ocorreu um erro ao deletar o veículo.')
    });
  }
}
