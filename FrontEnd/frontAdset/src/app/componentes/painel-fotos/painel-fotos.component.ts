import { Component, Input, OnInit } from '@angular/core';
import { FotoDto } from 'src/app/interfaces/Foto/FotoDto';
import { VeiculoService } from 'src/app/services/veiculo/veiculo-service.service';
import { environment } from 'src/environments/environment';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-painel-fotos',
  templateUrl: './painel-fotos.component.html',
  styleUrls: ['./painel-fotos.component.css']
})
export class PainelFotosComponent implements OnInit {
  fotos: FotoDto[] = [];
  previewPath: string | null = null;
previewTop = 0;
previewLeft = 0;
  constructor(private _service: VeiculoService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.carregarFotosVeiculo(id);

  }


  private carregarFotosVeiculo(id: number): void {
    this._service.ObterFotosVeiculo(id).subscribe({
      next: (fotos: FotoDto[]) => {
        this.fotos = fotos ?? [];
         this.gerarUrls();
      },
      error: (err) => {
        SwalHandler.showFalha("Falha", "Algo deu errado ao consultar as fotos do veiculo");
      }
    });
  }

private gerarUrls() {
this.fotos.forEach(foto=>{
  foto.path = environment.imagemRoute + foto.path;
})
}

onFotoChange(event: any, fotoId: number): void {
    const fileList: FileList | null = event.target.files;

    if (fileList && fileList.length > 0) {
      const file: File = fileList[0];
      
      console.log(`Substituir foto ID: ${fotoId}`);
      console.log('Arquivo selecionado:', file);
      event.target.value = null;
    }
  }



  trackByFotoId(index: number, foto: any): number {
    return foto.id;
  }

previewFoto(path: string, event: MouseEvent) {
  this.previewPath = path;

  const offset = 40; 
  this.previewTop = event.clientY + offset;
  this.previewLeft = event.clientX + offset;
}

closePreview() {
  this.previewPath = null;
}

}
