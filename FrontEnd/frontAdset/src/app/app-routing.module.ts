import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './componentes/home/home.component';
import {TelaCadastroComponent} from './componentes/tela-cadastro/tela-cadastro.component';
import {TelaEdicaoComponent} from './componentes/tela-edicao/tela-edicao.component';
import {PainelFotosComponent} from './componentes/painel-fotos/painel-fotos.component';



const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'cadastro', component: TelaCadastroComponent },
   { path: 'edicao', component: TelaEdicaoComponent },
    { path: 'fotos', component: PainelFotosComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
