import React, { useEffect, useState } from "react";
import Hero from "../components/Hero";
import PropertyCard from "../components/PropertyCard";
import { getProperties } from "../services/propertyService";
import type { PropertyDto, FilterDto } from "types";
import Zoom from "react-medium-image-zoom";
import "react-medium-image-zoom/dist/styles.css";

const Home: React.FC = () => {
    const [properties, setProperties] = useState<PropertyDto[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const [selectedProperty, setSelectedProperty] = useState<PropertyDto | null>(null);
    const [isDetailOpen, setIsDetailOpen] = useState(false);


    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10;

    const totalPages = Math.ceil(properties.length / itemsPerPage);
    const paginatedProperties = properties.slice(
        (currentPage - 1) * itemsPerPage,
        currentPage * itemsPerPage
    );

    const loadProperties = async (filters?: FilterDto) => {
        try {
            setLoading(true);
            setError(null);
            const data = await getProperties(filters);
            setProperties(data);
            setCurrentPage(1);
        } catch (err: any) {
            setError(err.message || "Error desconocido");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadProperties();
    }, []);

    const handleSearch = (filters: FilterDto) => {
        loadProperties(filters);
    };

    const handleViewDetail = async (id: string) => {
        try {
            setLoading(true);
            const res = await fetch(`http://localhost:5103/api/properties/${id}`);
            if (!res.ok) throw new Error("No se pudo obtener el detalle");

            const data: PropertyDto = await res.json();
            setSelectedProperty(data);
            setIsDetailOpen(true);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Hero onSearch={handleSearch} />

            <div className="container mx-auto p-6 px-4">
                <h1 className="text-xl sm:text-2xl font-bold mb-4">Featured Properties</h1>

                {loading && <p>Cargando propiedades...</p>}
                {error && <p className="text-red-600">{error}</p>}

                {!loading && !error && (
                    <>
                        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                            {paginatedProperties.map((property) => (
                                <PropertyCard
                                    key={property.id}
                                    property={property}
                                    onViewDetail={handleViewDetail}
                                />
                            ))}
                        </div>

                        {totalPages > 1 && (
                            <div className="flex justify-center items-center gap-4 mt-6">
                                <button
                                    onClick={() => setCurrentPage((p) => Math.max(p - 1, 1))}
                                    disabled={currentPage === 1}
                                    className="px-3 py-1 bg-gray-200 rounded disabled:opacity-50"
                                >
                                    Prev
                                </button>
                                <span>
                                    Page {currentPage} of {totalPages}
                                </span>
                                <button
                                    onClick={() => setCurrentPage((p) => Math.min(p + 1, totalPages))}
                                    disabled={currentPage === totalPages}
                                    className="px-3 py-1 bg-gray-200 rounded disabled:opacity-50"
                                >
                                    Next
                                </button>
                            </div>
                        )}
                    </>
                )}

                {isDetailOpen && selectedProperty && (
                    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center p-4 z-50">
                        <div className="bg-white rounded-lg shadow-lg max-w-3xl w-full relative p-6 overflow-y-auto max-h-[90vh]">
                            <button
                                onClick={() => setIsDetailOpen(false)}
                                className="absolute top-3 right-3 text-gray-500 hover:text-black"
                            >
                                ✖
                            </button>

                            <h2 className="text-2xl font-bold mb-2">{selectedProperty.name}</h2>

                            <Zoom>
                                <img
                                    src={selectedProperty.image || "/assets/default-house.jpg"}
                                    alt={selectedProperty.name}
                                    className="w-full max-h-[70vh] object-contain rounded mb-4 cursor-zoom-in"
                                />
                            </Zoom>

                            <p><strong>Dirección:</strong> {selectedProperty.address}</p>
                            <p><strong>Precio:</strong> ${selectedProperty.price.toLocaleString()}</p>

                            {selectedProperty.traces && selectedProperty.traces.length > 0 && (
                                <>
                                    <h3 className="mt-4 font-semibold">Historial de ventas</h3>
                                    <ul className="list-disc ml-5">
                                        {selectedProperty.traces.map((trace) => (
                                            <li key={trace.id}>
                                                {new Date(trace.dateSale).toLocaleDateString()} - {trace.name} - $
                                                {trace.value.toLocaleString()} (Impuesto: ${trace.tax.toLocaleString()})
                                            </li>
                                        ))}
                                    </ul>
                                </>
                            )}
                        </div>
                    </div>
                )}
            </div>
        </>
    );
};

export default Home;