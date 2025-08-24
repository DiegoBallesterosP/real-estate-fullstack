// src/services/propertyService.ts
import { API_BASE_URL } from "../utils/config";
import { PropertyDto } from "types";

export async function getProperties(filters?: Record<string, any>): Promise<PropertyDto[]> {
    const params = new URLSearchParams();

    if (filters?.name) params.append("name", filters.name);
    if (filters?.address) params.append("address", filters.address);
    if (filters?.minPrice) params.append("minPrice", String(filters.minPrice));
    if (filters?.maxPrice) params.append("maxPrice", String(filters.maxPrice));

    const queryString = params.toString();
    const url = queryString
        ? `${API_BASE_URL}/properties?${queryString}`
        : `${API_BASE_URL}/properties`;

    console.log("GET →", url);

    const res = await fetch(url);
    if (!res.ok) throw new Error(`Error ${res.status}: ${res.statusText}`);
    return res.json();
}

export async function getPropertyById(id: string): Promise<PropertyDto> {
    const url = `${API_BASE_URL}/properties/${id}`;
    console.log("GET →", url);

    const res = await fetch(url);
    if (!res.ok) throw new Error(`Error ${res.status}: ${res.statusText}`);
    return res.json();
}
