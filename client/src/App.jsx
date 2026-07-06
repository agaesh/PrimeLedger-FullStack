import './App.css'
import Sidebar from './Components/Sidebar/Sidebar'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Dashboard from './Pages/Dashboard/Dashboards';

function App() {
    return (
        <BrowserRouter>
            <div style={{ display: "flex", height: "100vh" }}>

                {/* Sidebar */}
                <Sidebar />

                {/* Main Content */}
                <div style={{ flex: 1, padding: "20px" }}>
                    <Routes>
                        <Route path="/" element={<Dashboard/>} />
                        <Route path="/invoice" element={<div>Create Invoice</div>} />
                        <Route path="/documents" element={<div>Documents</div>} />
                        <Route path="/directory" element={<div>Directory</div>} />
                    </Routes>
                </div>

            </div>
        </BrowserRouter>
    )
}

export default App