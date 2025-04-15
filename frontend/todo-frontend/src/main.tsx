import { createRoot } from 'react-dom/client'
import './index.css'
import {BrowserRouter, Route, Routes} from "react-router-dom";
import LoginPage from "./pages/auth/login-page.tsx";
import RegisterPage from "./pages/auth/register-page.tsx";

createRoot(document.getElementById('root')!).render(
 <BrowserRouter>
     <Routes>
         <Route path={"/login"} element={<LoginPage />} />
         <Route path={"register"} element={<RegisterPage />} />
     </Routes>
 </BrowserRouter>
)
