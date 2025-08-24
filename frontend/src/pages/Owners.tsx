// src/pages/Owners.tsx
import React, { useEffect, useState } from "react";
import { getOwners } from "../services/ownerService";
import { OwnerDto } from "types";

const Owners: React.FC = () => {
    const [owners, setOwners] = useState<OwnerDto[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10;

    useEffect(() => {
        setLoading(true);
        getOwners()
            .then(setOwners)
            .catch((err) => setError(err.message))
            .finally(() => setLoading(false));
    }, []);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentOwners = owners.slice(indexOfFirstItem, indexOfLastItem);
    const totalPages = Math.ceil(owners.length / itemsPerPage);

    const handlePrev = () => {
        if (currentPage > 1) setCurrentPage((p) => p - 1);
    };
    const handleNext = () => {
        if (currentPage < totalPages) setCurrentPage((p) => p + 1);
    };

    return (
        <div className="container mx-auto p-6 px-4">
            <h1 className="text-xl sm:text-2xl font-bold mb-4">Owners</h1>

            {loading && <p>Loading...</p>}
            {error && <p className="text-red-600">{error}</p>}

            {!loading && !error && (
                <>
                    <ul className="flex flex-wrap gap-4">
                        {currentOwners.map((owner) => (
                            <li
                                key={owner.id}
                                className="flex items-center gap-4 border p-4 rounded shadow max-w-sm w-full"
                            >
                                <img
                                    src={owner.photo || "/assets/default-user.jpg"}
                                    alt={owner.name}
                                    className="w-16 h-16 sm:w-20 sm:h-20 rounded-full object-cover flex-shrink-0"
                                />
                                <div className="flex flex-col">
                                    <span className="font-semibold">{owner.name}</span>
                                    <span className="text-gray-600">{owner.address}</span>
                                </div>
                            </li>
                        ))}
                    </ul>

                    <div className="flex justify-center items-center gap-4 mt-6">
                        <button
                            onClick={handlePrev}
                            disabled={currentPage === 1}
                            className="px-4 py-2 bg-gray-200 rounded disabled:opacity-50"
                        >
                            Prev
                        </button>
                        <span>
                            Page {currentPage} of {totalPages}
                        </span>
                        <button
                            onClick={handleNext}
                            disabled={currentPage === totalPages}
                            className="px-4 py-2 bg-gray-200 rounded disabled:opacity-50"
                        >
                            Next
                        </button>
                    </div>
                </>
            )}
        </div>
    );
};

export default Owners;