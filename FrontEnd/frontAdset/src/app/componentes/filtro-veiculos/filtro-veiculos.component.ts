import { Component, OnInit } from '@angular/core';
import { VeiculoService } from '../../services/veiculo-service.service';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { VeiculoFiltroDto } from 'src/app/interfaces/Veiculo/VeiculoFiltroDto';

@Component({
  selector: 'app-filtro-veiculos',
  templateUrl: './filtro-veiculos.component.html',
  styleUrls: ['./filtro-veiculos.component.css']
})
export class FiltroVeiculosComponent implements OnInit {

  constructor(private _service: VeiculoService) { }

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
  cores: string [] = []


  ngOnInit(): void {
    this.listarVeiculos();
    this.gerarAnos();
    this.gerarFaixasPreco();
    this.gerarCores();
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
