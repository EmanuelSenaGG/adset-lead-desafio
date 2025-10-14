import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OpcionalVeiculoDto } from '../../interfaces/Veiculo/OpcionalVeiculoDto';
import { VeiculoCadastrarDto } from '../../interfaces/Veiculo/VeiculoCadastrarDto';
import { VeiculoDto } from '../../interfaces/Veiculo/VeiculoDto';
import { VeiculoService } from '../../services/veiculo-service.service';
import { SwalHandler } from '../../utils/SwalHandler';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-form-editar',
  templateUrl: './form-editar.component.html',
  styleUrls: ['./form-editar.component.css']
})
export class FormEditarComponent implements OnInit {

  veiculoForm!: FormGroup;
  opcionaisDisponiveis: OpcionalVeiculoDto[] = [];
  opcionaisSelecionados: OpcionalVeiculoDto[] = [];


  constructor(
    private _service: VeiculoService,
    private router: Router,
    private fb: FormBuilder,
      private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.veiculoForm = this.fb.group({
      marca: ['', Validators.required],
      modelo: ['', Validators.required],
      ano: ['', [Validators.required, Validators.min(2000), Validators.max(2024)]],
      placa: ['', Validators.required],
      km: [''],
      cor: ['', Validators.required],
      preco: ['', Validators.required]
    });

    this.listarOpcionais();

    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.carregarVeiculo(id);
    }
  }

  listarOpcionais(): void {
    this._service.ListarOpcionais().subscribe({
      next: (dados) => this.opcionaisDisponiveis = dados,
      error: () => SwalHandler.showFalha('Erro', 'Falha ao carregar opcionais.')
    });
  }

  carregarVeiculo(id: number): void {
  this._service.ObterPorId(id).subscribe({
    next: (v: VeiculoDto) => {
      this.veiculoForm.patchValue({
        marca: v.marca,
        modelo: v.modelo,
        ano: v.ano,
        placa: v.placa,
        km: v.km,
        cor: v.cor,
        preco: v.preco
      });


      this.opcionaisSelecionados = v.opcionais ?? [];
    },
    error: () => SwalHandler.showFalha('Erro', 'Não foi possível carregar o veículo.')
  });
}
  adicionarOpcional(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const idSelecionado = Number(select.value);
    const opcional = this.opcionaisDisponiveis.find(o => o.opcionalId === idSelecionado);

    if (opcional && !this.opcionaisSelecionados.some(o => o.opcionalId === opcional.opcionalId)) {
      this.opcionaisSelecionados.push(opcional);
    }
    select.value = '';
  }

  removerOpcional(opcionalParaRemover: OpcionalVeiculoDto): void {
    this.opcionaisSelecionados = this.opcionaisSelecionados.filter(o => o.opcionalId !== opcionalParaRemover.opcionalId);
  }


  onSubmit(): void {

    if (this.veiculoForm.invalid) {
      this.veiculoForm.markAllAsTouched();
      return;
    }
    const opcionaisIds = this.opcionaisSelecionados.map(o => o.opcionalId.toString());

    const veiculoDto: VeiculoCadastrarDto = {
      marca: this.veiculoForm.value.marca,
      modelo: this.veiculoForm.value.modelo,
      ano: Number(this.veiculoForm.value.ano),
      placa: this.veiculoForm.value.placa,
      cor: this.veiculoForm.value.cor,
      preco: Number(this.veiculoForm.value.preco),
      km: this.veiculoForm.value.km ? Number(this.veiculoForm.value.km) : undefined,
      opcionais: opcionaisIds

    };

    this.editarVeiculo(veiculoDto);
  }

editarVeiculo(veiculoDto: VeiculoCadastrarDto): void {
  const id = Number(this.route.snapshot.paramMap.get('id'));

  this._service.editarVeiculo(id, veiculoDto).subscribe({
    next: () =>
      SwalHandler.showSucessoRedirecionamento(
        this.router,
        'Sucesso',
        'Veículo editado com sucesso!',
        '/veiculos' 
      ),
    error: () =>
      SwalHandler.showFalha('Falha', 'Ocorreu um erro ao editar o veículo.')
  });
}





}
