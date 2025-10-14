import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { VeiculoCadastrarDto } from '../interfaces/Veiculo/VeiculoCadastrarDto';
import { OpcionalDto } from '../interfaces/Opcional/OpcionalDto';
import { InformacoesVeiculosDto } from '../interfaces/Veiculo/InformacoesVeiculosDto';
import { VeiculoDto } from '../interfaces/Veiculo/VeiculoDto';
import { VeiculoFiltroDto } from '../interfaces/Veiculo/VeiculoFiltroDto';

@Injectable({
  providedIn: 'root'
})
export class VeiculoService {

  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) { }

  CadastrarVeiculo(veiculo: VeiculoCadastrarDto): Observable<any> {
    const formData = new FormData();
    formData.append('marca', veiculo.marca);
    formData.append('modelo', veiculo.modelo);
    formData.append('ano', veiculo.ano.toString());
    formData.append('placa', veiculo.placa);
    formData.append('cor', veiculo.cor);
    formData.append('preco', veiculo.preco.toString());

    if (veiculo.km) {
      formData.append('km', veiculo.km.toString());
    }

    if (veiculo.opcionais && veiculo.opcionais.length > 0) {
      veiculo.opcionais.forEach(opId => formData.append('Opcionais[]', opId));
    }

    if (veiculo.fotos && veiculo.fotos.length > 0) {
      veiculo.fotos.forEach(file => formData.append('Fotos[]', file, file.name));
    }

    return this.http.post<VeiculoCadastrarDto>(`${this.apiUrl}/cadastrar`, formData);
  }

  ListarOpcionais(): Observable<OpcionalDto[]> {
    return this.http.get<OpcionalDto[]>(`${this.apiUrl}/opcionais`);
  }

  ObterInformacoesVeiculos(): Observable<InformacoesVeiculosDto> {
    return this.http.get<InformacoesVeiculosDto>(`${this.apiUrl}/informacoes`);
  }

  ObterDadosVeiculoCard(id:number): Observable<VeiculoDto> {
    return this.http.get<VeiculoDto>(`${this.apiUrl}/${id}`);
  }

   ObterCores(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/cores`);
  }

  listarVeiculos(
    pagina: number = 1,
    tamanho: number = 10,
    filtros: any = {}
  ): Observable<VeiculoFiltroDto> {

    let params = new HttpParams()
      .set('pagina', pagina)
      .set('tamanhoPagina', tamanho);

    Object.keys(filtros || {}).forEach((key) => {
      const valor = filtros[key];
      if (valor !== null && valor !== undefined && valor !== '') {
        params = params.set(key, valor);
      }
    });

    return this.http.get<VeiculoFiltroDto>(this.apiUrl, { params });
  }
}


