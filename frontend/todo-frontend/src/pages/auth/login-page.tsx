import logo from "../../assets/images/logo.svg"
import React, {useState} from "react";
import {validateEmail, validatePassword} from "../../utils/validators/validator.ts";
import {clearError, triggerShake} from "../../utils/button-handlers/helpers.ts";
import apiClient from "../../utils/api-client/api-client.ts";
import {AuthResponse} from "../../interfaces/auth/auth-response.ts";
import {useAlerts} from "../../hooks/alert/use-alerts.ts";
import {useNavigate} from "react-router-dom";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPasswords, setShowPasswords] = useState(false);
    const {addAlert} = useAlerts();
    const navigate = useNavigate();

    const handleNextClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();

        const passwordMessage = validatePassword(password);
        const emailMessage = validateEmail(email);

        if (emailMessage.length > 0) {
            triggerShake('password', passwordMessage);
        } else {
            clearError('password');
        }

        if (passwordMessage.length > 0) {
            triggerShake('password', passwordMessage);
        } else {
            clearError('password');
        }

        if (passwordMessage.length === 0 && emailMessage.length === 0) {
            processLogin()
        }
    };

    const processLogin = () => {
        apiClient
            .post<AuthResponse>('Auth/Login', {
                password: password,
                email: email,
            })
            .then((response) => {
                if (response.status === 200) {
                    document.cookie = `token=${response.data.token}; path=/;`;
                    navigate('/home');
                } else {
                    addAlert(response.data.message!);
                }
            })
            .catch((error) => {
                const errorMessage = error.response?.data.Message || null;
                addAlert(errorMessage);
            });
    };

    return (
        <div className="auth-layout">
            <div className="auth-card">
                <div className="auth-card__left">
                    <img className="auth__logo" src={logo} alt="logo" />
                    <h1 className="auth__project-name">Project</h1>
                </div>
                <div className="auth-card__right">
                    <h2 className="auth__title">Login</h2>
                    <p className="auth__subtitle">Welcome back! Please enter your credentials to access your account.</p>
                    <form className="auth__form">
                        <div className="auth__form-fields">
                            <div className="auth__input-container">
                                <input className="auth__input" id="email" value={email}
                                       onChange={(e) => setEmail(e.target.value)}
                                       type="email" placeholder=" "/>
                                <label htmlFor="email" className="auth__input__label">Email</label>
                                <div id="email-error" className="auth__error-message hidden">
                                    <span>
                                        <svg
                                            aria-hidden="true"
                                            fill="currentColor"
                                            focusable="false"
                                            width="16px"
                                            height="16px"
                                            viewBox="0 0 24 24"
                                            xmlns={"https://www.w3.org/2000/svg"}
                                        >
                                            <path
                                                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"/>
                                        </svg>
                                    </span>
                                    <span id="email-message"/>
                                </div>
                            </div>
                            <div className="auth__input-container">
                                <input className="auth__input" id="password" value={password} onChange={(e) => setPassword(e.target.value)}
                                       type={showPasswords ? "text" : "password"} placeholder=" "/>
                                <label htmlFor="password" className="auth__input__label">Password</label>
                                <div id="password-error" className="auth__error-message hidden">
                                    <span>
                                        <svg
                                            aria-hidden="true"
                                            fill="currentColor"
                                            focusable="false"
                                            width="16px"
                                            height="16px"
                                            viewBox="0 0 24 24"
                                            xmlns={"https://www.w3.org/2000/svg"}
                                        >
                                            <path
                                                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"/>
                                        </svg>
                                    </span>
                                    <span id="password-message"/>
                                </div>
                            </div>
                        </div>
                        <button
                            type="button"
                            className="auth__toggle-button"
                            onClick={() => setShowPasswords(!showPasswords)}
                        >
                            {showPasswords ? "Hide Password" : "Show Password"}
                        </button>
                        <button type="button" className="auth__button" onClick={(e) => handleNextClick(e)}>Login</button>
                    </form>
                    <div className="auth__footer">
                        <span>Don't have an account?</span>
                        <a href="/register" className="auth__link">Sign up</a>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;