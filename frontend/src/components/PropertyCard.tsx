import React from "react";
import { PropertyDto } from "types";

interface Props {
    property: PropertyDto;
    onViewDetail: (id: string) => void;
}

const PropertyCard: React.FC<Props> = ({ property, onViewDetail }) => {
    return (
        <div className="border rounded-lg shadow hover:shadow-lg transition">
            <img
                src={property.image || "/assets/default-house.jpg"}
                alt={property.name}
                className="w-full h-48 object-cover rounded-t-lg"
            />
            <div className="p-4">
                <h3 className="font-semibold text-lg">{property.name}</h3>
                <p className="text-blue-600 font-bold">
                    ${property.price.toLocaleString()}
                </p>
                <p className="text-gray-500">{property.address}</p>
                <button
                    type="button" // ✅ evita el comportamiento de submit y el scroll al inicio
                    onClick={() => onViewDetail(property.id)}
                    className="mt-3 block w-full text-center bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                >
                    Ver Detalle
                </button>
            </div>
        </div>
    );
};

export default PropertyCard;