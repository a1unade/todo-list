import { createRoot } from 'react-dom/client'
import './index.css'
import {BrowserRouter, Route, Routes} from "react-router-dom";
import LoginPage from "./pages/auth/login-page.tsx";
import RegisterPage from "./pages/auth/register-page.tsx";
import NotFoundPage from "./pages/404";
import {AlertProvider} from "./contexts/alert-provider.tsx";
import Alert from "./components/alert";
import {AlertType} from "./types/alert/alert-type.ts";
import {useAlerts} from "./hooks/use-alerts.ts";
import store from "./store";
import {Provider} from "react-redux";

const App = () => {
    const { alerts, removeAlert } = useAlerts();

    return (
        <>
            <div className="alert-container">
                {alerts.map((alert: AlertType) => (
                    <Alert key={alert.id} message={alert.message} onClose={() => removeAlert(alert.id)} />
                ))}
            </div>
            <Routes>
                <Routes>
                    <Route path={"/login"} element={<LoginPage />} />
                    <Route path={"register"} element={<RegisterPage />} />
                    <Route path={"/not-found"} element={<NotFoundPage />} />
                </Routes>
            </Routes>
        </>
    );
}

createRoot(document.getElementById('root')!).render(
    <Provider store={store}>
        <AlertProvider>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </AlertProvider>
    </Provider>
);

export default App;