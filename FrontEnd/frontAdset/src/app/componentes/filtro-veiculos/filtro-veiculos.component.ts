import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { VeiculoService } from '../../services/veiculo-service.service';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { VeiculoFiltroDto } from 'src/app/interfaces/Veiculo/VeiculoFiltroDto';
import { OpcionalDto } from 'src/app/interfaces/Opcional/OpcionalDto';
import { PortalDto } from 'src/app/interfaces/Portal/PortalDto';

@Component({
  selector: 'app-filtro-veiculos',
  templateUrl: './filtro-veiculos.component.html',
  styleUrls: ['./filtro-veiculos.component.css']
})
export class FiltroVeiculosComponent implements OnInit {


  constructor(private _service: VeiculoService) { }
  @ViewChild('placa') placa!: ElementRef;
  @ViewChild('marca') marca!: ElementRef;
  @ViewChild('modelo') modelo!: ElementRef;
  @ViewChild('anomin') anoMin!: ElementRef;
  @ViewChild('anomax') anoMax!: ElementRef;
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
  portais: PortalDto[]=[];



  ngOnInit(): void {
    this.listarVeiculos();
    this.gerarAnos();
    this.gerarFaixasPreco();
    this.gerarCores();
    this.obterPortais();

  }


  gerarAnos(): void {
    const anoInicial = 2000;
    const anoFinal = new Date().getFullYear();
    this.anos = Array.from({ length: anoFinal - anoInicial + 1 }, (_, i) => anoInicial + i);
  }

  gerarCores(): void {
    this._service.ObterCores().subscribe({
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


   obterPortais(): void {
    this._service.ObterCores().subscribe({
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
  
  listarVeiculos(): void {
    this._service.listarVeiculos(this.paginaAtual, this.tamanhoPagina, this.filtros).subscribe({
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

obterPrecoMax(valorMin: string): string {
  switch (valorMin) {
    case "0":
    case "90001": return "0";
    case "10000": return "50000";
    case "50000": return "90000";
    default: return "0";
  }
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


  aplicarFiltros(filtros: any) {
    this.filtros = filtros;
    this.paginaAtual = 1;
    this.listarVeiculos();
  }

  onTamanhoPaginaChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.tamanhoPagina = Number(selectElement.value);
    this.paginaAtual = 1;
    this.listarVeiculos();
  }
}











