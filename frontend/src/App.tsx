import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Owners from "./pages/Owners";

export default function App() {
  return (
    <div className="min-h-screen bg-white font-sans text-gray-900">
      <BrowserRouter>
        <Navbar />
        <main className="container mx-auto py-6">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/owners" element={<Owners />} />
          </Routes>
        </main>
      </BrowserRouter>
    </div>
  );
}