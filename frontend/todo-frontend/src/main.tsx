import { createRoot } from 'react-dom/client'
import './index.css'
import {BrowserRouter, Route, Routes} from "react-router-dom";
import LoginPage from "./pages/auth/login-page.tsx";
import RegisterPage from "./pages/auth/register-page.tsx";
import NotFoundPage from "./pages/404";
import {AlertProvider} from "./contexts/alert/alert-provider.tsx";
import Alert from "./components/alert";
import {AlertType} from "./types/alert/alert-type.ts";
import {useAlerts} from "./hooks/alert/use-alerts.ts";
import store from "./store";
import {Provider} from "react-redux";
import Home from "./pages/home";
import {LoadingProvider} from "./contexts/loading/loading-provider.tsx";
import {useLoading} from "./hooks/loading/use-loading.ts";
import { ScaleLoader } from "react-spinners";
import ProjectsPage from "./pages/projects";

const App = () => {
    const { alerts, removeAlert } = useAlerts();
    const { loading } = useLoading();

    return (
        <>
            {loading && (
                <div className="loader-overlay">
                    <div className="loader">
                        <ScaleLoader loading={loading} color="#4a90e2" />
                    </div>
                </div>
            )}
            <div className="alert-container">
                {alerts.map((alert: AlertType) => (
                    <Alert key={alert.id} message={alert.message} onClose={() => removeAlert(alert.id)} />
                ))}
            </div>
            <Routes>
                <Route path={"/home"} element={<Home />} />
                <Route path={"/login"} element={<LoginPage />} />
                <Route path={"/register"} element={<RegisterPage />} />
                <Route path={"/projects"} element={<ProjectsPage />} />
                <Route path={"/not-found"} element={<NotFoundPage />} />
                <Route path={"*"} element={<NotFoundPage />} />
            </Routes>
        </>
    );
}

createRoot(document.getElementById('root')!).render(
    <Provider store={store}>
        <LoadingProvider>
            <AlertProvider>
                <BrowserRouter>
                    <App />
                </BrowserRouter>
            </AlertProvider>
        </LoadingProvider>
    </Provider>
);

export default App;