import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './componentes/home/home.component';
import {TelaCadastroComponent} from './componentes/tela-cadastro/tela-cadastro.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'cadastro', component: TelaCadastroComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
