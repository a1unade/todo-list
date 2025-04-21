import React, {useEffect, useState} from "react";
import {validatePassword} from "../../../utils/validators/validator.ts";
import {clearError, generateJwt, triggerShake} from "../../../utils/button-handlers/helpers.ts";
import errors from "../../../utils/validators/error-messages.ts";
import {jwtDecode} from "jwt-decode";
import {RegistrationTokenData, RegistrationTokenPayload} from "../../../interfaces/jwt/registration-token-payload.ts";
import {useLoading} from "../../../hooks/loading/use-loading.ts";
import {useAlerts} from "../../../hooks/alert/use-alerts.ts";
import {useNavigate, useSearchParams} from "react-router-dom";

const RegisterPasswordStep = (props: {
    step: number,
    setStep: React.Dispatch<React.SetStateAction<number>>
}) => {
    const {step, setStep} = props;

    const [searchParams] = useSearchParams();
    const [token, setToken] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [confirm, setConfirm] = useState<string>('');
    const [showPasswords, setShowPasswords] = useState(false);
    const navigate = useNavigate();
    const {addAlert} = useAlerts();
    const {startLoading, stopLoading} = useLoading();

    useEffect(() => {
        const token = searchParams.get('t');

        if (token) {
            setToken(token);
        }
    }, [searchParams]);

    const handleNextClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();

        const passwordMessage = validatePassword(password);

        if (passwordMessage.length > 0) {
            triggerShake('password', passwordMessage);
        } else {
            clearError('password');
        }

        if (password !== confirm) {
            triggerShake('confirm', errors.passwordConfirm);
        } else {
            clearError('confirm');
        }

        if (passwordMessage.length === 0 && password === confirm) {
            const jwtPayload = jwtDecode<RegistrationTokenPayload>(token);
            let parsedData : RegistrationTokenData | string = jwtPayload.Data;

            if (typeof parsedData === "string") {
                parsedData = JSON.parse(parsedData) as RegistrationTokenData;
            }

            const data = {
                name: parsedData.name,
                surname: parsedData.surname,
                birthDate: parsedData.birthDate,
                gender: parsedData.gender,
                password: password
            }

            setStep(step + 1);
            generateJwt(step, data, addAlert, navigate, startLoading, stopLoading);
        }
    };

    return (
        <div className="auth-card__right">
            <h2 className="auth__title">Create Account</h2>
            <p className="auth__subtitle">Almost there! Let's secure your new account with a password.</p>
            <form className="auth__form">
                <div className="auth__form-fields">
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
                    <div className="auth__input-container">
                        <input className="auth__input" value={confirm} onChange={(e) => setConfirm(e.target.value)}
                               id="confirm" type={showPasswords ? "text" : "password"} placeholder=" "/>
                        <label htmlFor="confirm" className="auth__input__label">Confirm</label>
                        <div id="confirm-error" className="auth__error-message hidden">
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
                            <span id="confirm-message"/>
                        </div>
                    </div>
                    <button
                        type="button"
                        className="auth__toggle-button"
                        onClick={() => setShowPasswords(!showPasswords)}
                    >
                        {showPasswords ? "Hide Passwords" : "Show Passwords"}
                    </button>
                </div>
                <button className="auth__button" type="button" onClick={(e) => handleNextClick(e)}>Next</button>
            </form>
        </div>
    );
};

export default RegisterPasswordStep;