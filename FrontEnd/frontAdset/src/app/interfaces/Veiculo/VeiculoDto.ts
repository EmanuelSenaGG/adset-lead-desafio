import { PacotePortalDto } from "../Pacote/PacotePortalDto";
import { OpcionalVeiculoDto } from "../Veiculo/OpcionalVeiculoDto";
export interface VeiculoDto {
  id: number;
  marca: string;
  modelo: string;
  ano: number;
  placa: string;
  km: number | null;
  cor: string;
  preco: number;
  fotos: string[]; 
  opcionais: OpcionalVeiculoDto[];
  pacotesPortal: PacotePortalDto[];
}