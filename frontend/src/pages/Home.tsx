import React, { useEffect, useState } from "react";
import Hero from "../components/Hero";
import PropertyCard from "../components/PropertyCard";
import { getProperties, getPropertyById } from "../services/propertyService";
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
            setError(err.message || "Error loading properties");
            setProperties([]);
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
            const property = await getPropertyById(id);
            setSelectedProperty(property);
            setIsDetailOpen(true);
        } catch (err: any) {
            setError(err.message || "Error loading property details");
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Hero onSearch={handleSearch} />

            <div className="container mx-auto p-6 px-4">
                <h1 className="text-xl sm:text-2xl font-bold mb-4">Featured Properties</h1>

                {loading && (
                    <div className="flex justify-center items-center py-8">
                        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
                    </div>
                )}
                
                {error && (
                    <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4">
                        {error}
                    </div>
                )}

                {!loading && !error && properties.length === 0 && (
                    <div className="text-center py-8">
                        <p className="text-gray-500 text-lg">No properties found</p>
                        <button
                            onClick={() => loadProperties()}
                            className="mt-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                        >
                            Show All Properties
                        </button>
                    </div>
                )}

                {!loading && !error && properties.length > 0 && (
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
                                    className="px-4 py-2 bg-gray-200 rounded disabled:opacity-50 hover:bg-gray-300 transition"
                                >
                                    Previous
                                </button>
                                <span className="text-sm font-medium">
                                    Page {currentPage} of {totalPages}
                                </span>
                                <button
                                    onClick={() => setCurrentPage((p) => Math.min(p + 1, totalPages))}
                                    disabled={currentPage === totalPages}
                                    className="px-4 py-2 bg-gray-200 rounded disabled:opacity-50 hover:bg-gray-300 transition"
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
                                className="absolute top-3 right-3 text-gray-500 hover:text-black text-2xl"
                            >
                                ✖
                            </button>

                            <h2 className="text-2xl font-bold mb-4">{selectedProperty.name}</h2>

                            <Zoom>
                                <img
                                    src={selectedProperty.image || "/assets/default-house.jpg"}
                                    alt={selectedProperty.name}
                                    className="w-full h-64 object-cover rounded mb-4 cursor-zoom-in"
                                />
                            </Zoom>

                            <div className="space-y-2">
                                <p><strong>Address:</strong> {selectedProperty.address}</p>
                                <p><strong>Price:</strong> ${selectedProperty.price.toLocaleString()}</p>
                                <p><strong>Owner ID:</strong> {selectedProperty.idOwner}</p>
                            </div>

                            {selectedProperty.traces && selectedProperty.traces.length > 0 && (
                                <>
                                    <h3 className="mt-6 font-semibold text-lg">Sales History</h3>
                                    <ul className="list-disc ml-5 mt-2 space-y-1">
                                        {selectedProperty.traces.map((trace) => (
                                            <li key={trace.id} className="text-sm">
                                                {new Date(trace.dateSale).toLocaleDateString()} - {trace.name} - $
                                                {trace.value.toLocaleString()} (Tax: ${trace.tax.toLocaleString()})
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