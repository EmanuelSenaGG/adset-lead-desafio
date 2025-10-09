import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CabecalhoComponent } from './componentes/cabecalho/cabecalho.component';
import { PainelEstatisticasComponent } from './componentes/painel-estatisticas/painel-estatisticas.component';
import { FiltroVeiculosComponent } from './componentes/filtro-veiculos/filtro-veiculos.component';
import { BarraAcoesComponent } from './componentes/barra-acoes/barra-acoes.component';
import { ListaVeiculosComponent } from './componentes/lista-veiculos/lista-veiculos.component';
import { CardVeiculoComponent } from './componentes/card-veiculo/card-veiculo.component';
import { PaginacaoComponent } from './componentes/paginacao/paginacao.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BotaoExportarComponent } from './componentes/botao-exportar/botao-exportar.component';
import { BotaoCadastrarComponent } from './componentes/botao-cadastrar/botao-cadastrar.component';
import { BotaoSalvarComponent } from './componentes/botao-salvar/botao-salvar.component';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [
    AppComponent,
    CabecalhoComponent,
    PainelEstatisticasComponent,
    FiltroVeiculosComponent,
    BarraAcoesComponent,
    ListaVeiculosComponent,
    CardVeiculoComponent,
    PaginacaoComponent,
    BotaoExportarComponent,
    BotaoCadastrarComponent,
    BotaoSalvarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule ,
     MatTooltipModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
