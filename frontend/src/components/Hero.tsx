import React, { useState } from "react";
import { FilterDto } from "types";

interface HeroProps {
    onSearch: (filters: FilterDto) => void;
}

const Hero: React.FC<HeroProps> = ({ onSearch }) => {
    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [minPrice, setMinPrice] = useState("");
    const [maxPrice, setMaxPrice] = useState("");

    const cleanPriceInput = (value: string): string => {
        return value.replace(/[.,\-]/g, '').replace(/[^0-9]/g, '');
    };

    const handlePriceChange = (setter: React.Dispatch<React.SetStateAction<string>>) => 
        (e: React.ChangeEvent<HTMLInputElement>) => {
            const cleanedValue = cleanPriceInput(e.target.value);
            setter(cleanedValue);
        };

    const handleSearch = () => {
        onSearch({
            name: name.trim() || undefined,
            address: address.trim() || undefined,
            minPrice: minPrice ? Number(minPrice) : undefined,
            maxPrice: maxPrice ? Number(maxPrice) : undefined,
        });
    };

    const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === '-' || e.key === 'e' || e.key === 'E' || e.key === '+' || e.key === '.') {
            e.preventDefault();
        }
    };

    const handleKeyPressEnter = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === 'Enter') {
            handleSearch();
        }
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
                        placeholder="Search by name"
                        className="flex-1 min-w-[200px] border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        onKeyPress={handleKeyPressEnter}
                    />
                    <input
                        type="text"
                        placeholder="Search by address"
                        className="flex-1 min-w-[200px] border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                        onKeyPress={handleKeyPressEnter}
                    />
                    <input
                        type="number"
                        placeholder="Min. Price"
                        className="w-28 border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500 [appearance:textfield] [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none"
                        value={minPrice}
                        onChange={handlePriceChange(setMinPrice)}
                        onKeyPress={handleKeyPress}
                        min="0"
                    />
                    <input
                        type="number"
                        placeholder="Max. Price"
                        className="w-28 border border-gray-300 p-2 rounded text-gray-800 focus:outline-none focus:ring-2 focus:ring-blue-500 [appearance:textfield] [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none"
                        value={maxPrice}
                        onChange={handlePriceChange(setMaxPrice)}
                        onKeyPress={handleKeyPress}
                        min="0"
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