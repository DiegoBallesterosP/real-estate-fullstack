import type { TraceDto } from "./TraceDto";

export interface PropertyDto {
    id: string;
    idOwner: string;
    name: string;
    address: string;
    price: number;
    image: string;
    traces?: TraceDto[];
}