import React, { useState } from "react";
import { Link, NavLink } from "react-router-dom";

const Navbar: React.FC = () => {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <nav className="bg-white shadow-md sticky top-0 z-50">
            <div className="container mx-auto px-4 py-4 flex items-center justify-between relative">
                <Link
                    to="/"
                    className="text-2xl font-extrabold tracking-tight text-blue-700"
                >
                    Home
                </Link>

                <button
                    className="md:hidden text-gray-700 focus:outline-none"
                    onClick={() => setIsOpen(!isOpen)}
                >
                    <svg
                        className="w-6 h-6"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                    >
                        {isOpen ? (
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                strokeWidth={2}
                                d="M6 18L18 6M6 6l12 12"
                            />
                        ) : (
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                strokeWidth={2}
                                d="M4 6h16M4 12h16M4 18h16"
                            />
                        )}
                    </svg>
                </button>

                <div
                    className={`absolute md:static top-full left-0 w-full md:w-auto bg-white md:bg-transparent shadow-md md:shadow-none transition-all duration-300 ${isOpen ? "block" : "hidden"
                        } md:flex md:items-center md:gap-6`}
                >
                    <ul className="flex flex-col md:flex-row gap-4 px-4 py-4 md:p-0 text-lg font-medium md:absolute md:left-1/2 md:-translate-x-1/2">
                        <li>
                            <NavLink
                                to="/owners"
                                className={({ isActive }) =>
                                    `hover:text-blue-600 transition-colors ${isActive
                                        ? "text-blue-600"
                                        : "text-gray-700"
                                    }`
                                }
                            >
                                Owners
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;