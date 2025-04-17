import logo from "../../assets/images/logo.svg"
import {useNavigate, useSearchParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {jwtDecode} from "jwt-decode";
import {RegistrationTokenPayload} from "../../interfaces/jwt/registration-token-payload.ts";
import RegisterNameStep from "./components/register-name-step.tsx";

const RegisterPage = () => {
    // Текущий шаг регистрации, трекаем валидность токена, мало ли
    const [step, setStep] = useState(1);
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();

    // TODO: сделать сохранение шага при его изменении в localStorage, функцию короче надо

    useEffect(() => {
        // jwt токен для шагов при регистрации
        // хранит в себе информацию о текущем шаге и данные, которые пользователь уже вводил
        const token = searchParams.get('t');

        // Токена нет, делаем регистрацию с первого шага
        if(!token) {
            setStep(1);
            return;
        }

        const jwtPayload = jwtDecode<RegistrationTokenPayload>(token);
        if (jwtPayload.exp * 1000 < Date.now()) {
            // Токен протух, кидаем чела на первый шаг либо страницу с ошибкой !! надо подумать
            return;
        }

        if (jwtPayload.step !== step) {
            // Токен кривой, шаги не совпали, кидаем 404 страницу, пусть думает над своим поведением !!
            navigate("/not-found");
            return;
        }

        // Если все ок, то надо делать стор с пользователем
        // Вызов кастомного хука useActions, прокинуть data в стор, пусть сам дальше разбирается
    }, [searchParams, step, navigate]);

    // Рендерим правую часть страницы с регистрацией
    const renderCurrentStep = () => {
        switch (step) {
            case 1:
                return (<RegisterNameStep />);
        }
    }

    return (
        <div className="auth-layout">
            <div className="auth-card">
                <div className="auth-card__left">
                    <img className="auth__logo" src={logo} alt="logo" />
                    <h1 className="auth__project-name">Project</h1>
                </div>
                {renderCurrentStep()}
            </div>
        </div>
    );
};

export default RegisterPage;