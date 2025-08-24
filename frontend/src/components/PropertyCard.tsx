import React from "react";
import { PropertyDto } from "types";

interface Props {
    property: PropertyDto;
    onViewDetail: (id: string) => void;
}

const PropertyCard: React.FC<Props> = ({ property, onViewDetail }) => {
    const handleImageError = (e: React.SyntheticEvent<HTMLImageElement, Event>) => {
        e.currentTarget.src = "/assets/default-house.jpg";
    };

    return (
        <div className="border rounded-lg shadow hover:shadow-lg transition duration-300">
            <img
                src={property.image || "/assets/default-house.jpg"}
                alt={property.name}
                className="w-full h-48 object-cover rounded-t-lg"
                onError={handleImageError}
            />
            <div className="p-4">
                <h3 className="font-semibold text-lg mb-2 line-clamp-2">{property.name}</h3>
                <p className="text-blue-600 font-bold text-xl mb-2">
                    ${property.price.toLocaleString()}
                </p>
                <p className="text-gray-500 text-sm mb-4 line-clamp-2">{property.address}</p>
                <button
                    type="button"
                    onClick={() => onViewDetail(property.id)}
                    className="w-full bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition duration-200 font-medium"
                >
                    View Details
                </button>
            </div>
        </div>
    );
};

export default PropertyCard;