// src/services/ownerService.ts
import { API_BASE_URL } from "../utils/config";
import { OwnerDto } from "types";

export const getOwners = async (): Promise<OwnerDto[]> => {
    const url = `${API_BASE_URL}/owners`;
    console.log("GET →", url);

    const response = await fetch(url);
    if (!response.ok) throw new Error("Error fetching owners");
    return response.json();
};