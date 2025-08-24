import React, { useState } from "react";
import { FilterDto } from "types";

interface HeroProps {
    onSearch: (filters: FilterDto) => void;
}

const Hero: React.FC<HeroProps> = ({ onSearch }) => {
    const [name, setName] = useState("");
    const [minPrice, setMinPrice] = useState("");
    const [maxPrice, setMaxPrice] = useState("");

    const handleSearch = () => {
        onSearch({
            name: name || undefined,
            minPrice: minPrice ? Number(minPrice) : undefined,
            maxPrice: maxPrice ? Number(maxPrice) : undefined,
        });
    };

    return (
        <div
            className="relative bg-cover bg-bottom h-[500px] flex flex-col items-center justify-center text-white"
            style={{ backgroundImage: "url('/assets/house.jpg')" }}
        >
            <div className="text-center w-full flex flex-col items-center gap-6">
                <h1 className="text-4xl md:text-5xl font-extrabold drop-shadow-xl">
                    Find Your Dream Home
                </h1>

                <div className="bg-white bg-opacity-95 p-4 rounded-lg flex flex-wrap gap-2 shadow-lg w-11/12 md:w-2/3 justify-center">
                    <input
                        type="text"
                        placeholder="Search by name or address"
                        className="flex-1 min-w-[220px] border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                    <input
                        type="number"
                        placeholder="Min. Price"
                        className="w-28 border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500 [appearance:textfield] [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none"
                        value={minPrice}
                        onChange={(e) => setMinPrice(e.target.value)}
                    />
                    <input
                        type="number"
                        placeholder="Max. Price"
                        className="w-28 border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500 [appearance:textfield] [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none"
                        value={maxPrice}
                        onChange={(e) => setMaxPrice(e.target.value)}
                    />
                    <button
                        onClick={handleSearch}
                        className="bg-blue-600 text-white px-5 py-2 rounded font-semibold hover:bg-blue-700 transition"
                    >
                        Search
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Hero;