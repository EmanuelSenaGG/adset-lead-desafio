import { PacoteDto } from "../Pacote/PacoteDto";

export interface PortalDto {
  Id: number;
  Nome: string;
  pacotes: PacoteDto[];
}