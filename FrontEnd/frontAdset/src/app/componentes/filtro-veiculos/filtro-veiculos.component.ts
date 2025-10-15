import { Component, OnInit, ElementRef, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { VeiculoFiltroDto } from 'src/app/interfaces/Veiculo/VeiculoFiltroDto';
import { AtualizarRelacaoVeiculoPacotePortalDto } from 'src/app/interfaces/Veiculo/AtualizarRelacaoVeiculoPacotePortalDto';
import { PortalDto } from 'src/app/interfaces/Portal/PortalDto';
import { EdicaoService } from 'src/app/services/Triggers/edicao.service';
import { PortalService } from 'src/app/services/portal/portal.service';
import { CardVeiculoComponent } from '../card-veiculo/card-veiculo.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-filtro-veiculos',
  templateUrl: './filtro-veiculos.component.html',
  styleUrls: ['./filtro-veiculos.component.css']
})
export class FiltroVeiculosComponent implements OnInit {

  private salvarSubscription!: Subscription;
  constructor(
    private _veiculoService: VeiculoService,
    private _portalService: PortalService,
    private edicaoService: EdicaoService) { }

  @ViewChildren('cardveiculo') cardVeiculos!: QueryList<CardVeiculoComponent>;
  @ViewChild('placa') placa!: ElementRef;
  @ViewChild('marca') marca!: ElementRef;
  @ViewChild('modelo') modelo!: ElementRef;
  @ViewChild('anoMin') anoMin!: ElementRef;
  @ViewChild('anoMax') anoMax!: ElementRef;
  @ViewChild('preco') preco!: ElementRef;
  @ViewChild('fotos') fotos!: ElementRef;
  @ViewChild('opcionais') opcional!: ElementRef;
  @ViewChild('cor') cor!: ElementRef;

  paginacaoVeiculos: VeiculoFiltroDto = {
    itens: [],
    paginaAtual: 1,
    tamanhoPagina: 10,
    totalRegistros: 0,
    totalPaginas: 0
  };

  paginaAtual = 1;
  tamanhoPagina = 10;
  ordenacao: string = '';
  filtros: any = {};
  anos: number[] = [];
  faixasPreco: { label: string, valorMin: number }[] = [];
  cores: string[] = [];
  portais: PortalDto[] = [];



  ngOnInit(): void {
    this.gerarAnos();
    this.gerarFaixasPreco();
    this.gerarCores();
    this.obterPortais();
    this.listarVeiculos();
    this.salvarSubscription = this.edicaoService.salvarTrigger$.subscribe(() => {
      this.processarSalvamentoEmMassa();
    });
  }

  ngOnDestroy(): void {
    this.salvarSubscription.unsubscribe();
  }

  private processarSalvamentoEmMassa(): void {
    if (!this.cardVeiculos) return;


    const payloadFinal: AtualizarRelacaoVeiculoPacotePortalDto[] = this.cardVeiculos.toArray()
      .map(card => card.coletarDadosDosPortais())
      .reduce((acumulador, arrayAtual) => acumulador.concat(arrayAtual), []);

      this.editarVinculos(payloadFinal)
    


  }


  gerarAnos(): void {
    const anoInicial = 2000;
    const anoFinal = new Date().getFullYear();
    this.anos = Array.from({ length: anoFinal - anoInicial + 1 }, (_, i) => anoInicial + i);
  }

  gerarCores(): void {
    this._veiculoService.ObterCores().subscribe({
      next: (dados) => {
        this.cores = dados;
      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar as cores'
        );
      }
    });
  }

private editarVinculos(dto: AtualizarRelacaoVeiculoPacotePortalDto[]) {
    this._veiculoService.atualizarVinculos(dto).subscribe({
        next: () => {
            SwalHandler.showSucesso('Sucesso', 'Vínculos editados com sucesso!');
            this.listarVeiculos(); 
        },
        error: () => {
            SwalHandler.showFalha('Falha', 'Ocorreu um erro ao processar os vínculos.');
        }
    });
}

  obterPortais(): void {
    this._portalService.obterPortais().subscribe({
      next: (dados) => {
        this.portais = dados;

      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar os portais'
        );
      }
    });
  }

  gerarFaixasPreco(): void {
    this.faixasPreco = [
      { label: 'Selecione', valorMin: 0 },
      { label: '10 mil a 50 mil', valorMin: 10000 },
      { label: '50 mil a 90 mil', valorMin: 50000 },
      { label: '+ 90 mil', valorMin: 90001 },

    ];
  }



  obterPrecoMax(valorMin: string): string {
    switch (valorMin) {
      case "0":
      case "90001": return "0";
      case "10000": return "50000";
      case "50000": return "90000";
      default: return "0";
    }
  }

  listarVeiculos(): void {

    this._veiculoService.listarVeiculos(this.paginaAtual, this.tamanhoPagina, this.filtros).subscribe({
      next: (dados) => {
        this.paginacaoVeiculos = dados;
      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar os veiculos'
        );
      }
    });
  }

  buscarClick(): void {

    this.filtros = {
      placa: this.placa.nativeElement.value || null,
      marca: this.marca.nativeElement.value || null,
      modelo: this.modelo.nativeElement.value || null,
      anoMin: this.anoMin.nativeElement.value || null,
      anoMax: this.anoMax.nativeElement.value || null,
      precoMin: this.preco.nativeElement.value || null,
      precoMax: this.obterPrecoMax(this.preco.nativeElement.value),
      fotos: this.fotos.nativeElement.value || null,
      opcional: this.opcional.nativeElement.value || null,
      cor: this.cor.nativeElement.value || null,
    };

    Object.keys(this.filtros).forEach(
      (k) => (this.filtros[k] === null || this.filtros[k] === '') && delete this.filtros[k]
    );

    this.paginaAtual = 1;
    this.listarVeiculos();
  }

  ordenar(campo: string) {

    const asc = this.ordenacao === campo ? false : true;
    this.ordenacao = asc ? campo : campo + '_desc';

    this.paginacaoVeiculos.itens.sort((a, b) => {
      let valorA: any;
      let valorB: any;

      switch (campo) {
        case 'marcaModelo':
          valorA = a.marca + ' ' + a.modelo;
          valorB = b.marca + ' ' + b.modelo;
          break;
        case 'ano':
          valorA = a.ano;
          valorB = b.ano;
          break;
        case 'preco':
          valorA = a.preco;
          valorB = b.preco;
          break;
        default:
          return 0;
      }

      if (valorA < valorB) return asc ? -1 : 1;
      if (valorA > valorB) return asc ? 1 : -1;
      return 0;
    });
  }


  onPaginaMudou(pagina: number) {
    this.paginaAtual = pagina;
    this.listarVeiculos();
  }


  // aplicarFiltros(filtros: any) {
  //   this.filtros = filtros;
  //   this.paginaAtual = 1;
  //   this.listarVeiculos();
  // }

  onTamanhoPaginaChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.tamanhoPagina = Number(selectElement.value);
    this.paginaAtual = 1;
    this.listarVeiculos();
  }
}











