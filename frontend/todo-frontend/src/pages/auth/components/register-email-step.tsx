import React, {useEffect, useState} from "react";
import {jwtDecode} from "jwt-decode";
import {RegistrationTokenData, RegistrationTokenPayload} from "../../../interfaces/jwt/registration-token-payload.ts";
import apiClient from "../../../utils/api-client/api-client.ts";
import {AuthResponse} from "../../../interfaces/auth/auth-response.ts";
import {useAlerts} from "../../../hooks/alert/use-alerts.ts";
import {clearError, generateJwt, triggerShake} from "../../../utils/button-handlers/helpers.ts";
import {useNavigate, useSearchParams} from "react-router-dom";
import {useLoading} from "../../../hooks/loading/use-loading.ts";
import {validateEmail} from "../../../utils/validators/validator.ts";
import errors from "../../../utils/validators/error-messages.ts";


const RegisterEmailStep = (props: {
    step: number,
    setStep: React.Dispatch<React.SetStateAction<number>>
}) => {
    const {step, setStep} = props;

    const [searchParams] = useSearchParams();
    const [token, setToken] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [code, setCode] = useState<number>(0);
    const [isCodeSent, setIsCodeSent] = useState(false);
    const [showCodeInput, setShowCodeInput] = useState(false);
    const [codeLegit, setCodeLegit] = useState(false);
    const {addAlert} = useAlerts();
    const navigate = useNavigate();
    const {startLoading, stopLoading} = useLoading();

    useEffect(() => {
        const token = searchParams.get('t');

        if (token) {
            setToken(token);
        }
    }, [searchParams]);

    const processAuth = async (jwtPayload: RegistrationTokenData) => {
        const [day, month, year] = jwtPayload.birthDate!.split("-");
        apiClient
            .post<AuthResponse>('Auth/Auth', {
                password: jwtPayload.password,
                email: email,
                name: jwtPayload.name,
                surname: jwtPayload.surname,
                gender: jwtPayload.gender,
                dateOfBirth: `${year}-${month}-${day}`,
                country: 'Россия',
            })
            .then((response) => {
                if (response.status === 200) {
                    setIsCodeSent(true);
                } else {
                    addAlert("An error occurred. Please try again later.");
                }
            })
            .catch((error) => {
                const errorMessage = error.response?.data.Error || null;
                addAlert(errorMessage);
            });
    };

    const verifyCode = async () => {
        apiClient
            .post('User/CodeCheckForEmail', {
                email: email,
                code: code.toString(),
            })
            .then((response) => {
                if (response.status === 200) {
                    setCodeLegit(true);
                } else {
                    addAlert("Invalid code. Please try again.");
                }
            });
    }

    const handleNextClick = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();

        const emailMessage = validateEmail(email);
        const jwtPayload = jwtDecode<RegistrationTokenPayload>(token);
        let parsedData : RegistrationTokenData | string = jwtPayload.Data;

        if (typeof parsedData === "string") {
            parsedData = JSON.parse(parsedData) as RegistrationTokenData;
        }

        if (emailMessage.length > 0) {
            triggerShake('email', emailMessage);
            return;
        } else {
            clearError('email');
        }

        if (code.toString().length < 5 && isCodeSent) {
            if (code.toString().length < 5) {
                triggerShake('code', errors.emptyCode);
            } else {
                triggerShake('code', errors.invalidCode);
            }

            return;
        } else if (isCodeSent) {
            clearError('code');
        }

        if (!isCodeSent) {
            try {
                await processAuth(parsedData);

                setTimeout(() => {
                    setShowCodeInput(true);
                }, 5000);

            } catch (error) {
                console.error("Failed to send code:", error);
                addAlert("Failed to send code. Please try again.");
            }
        } else {
            await verifyCode();
            if (codeLegit) {
                const data = {
                    name: parsedData.name,
                    surname: parsedData.surname,
                    birthDate: parsedData.birthDate,
                    gender: parsedData.gender,
                    password: parsedData.password
                }

                setStep(step + 1);
                generateJwt(step, data, addAlert, navigate, startLoading, stopLoading);
            } else {
                addAlert("Invalid code. Please try again.");
            }
        }
    };

    return (
        <div className="auth-card__right">
            <h2 className="auth__title">Create Account</h2>
            <p className="auth__subtitle">Enter your email. We’ll send a confirmation code to verify it.</p>
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
                    {showCodeInput && (
                        <div className="auth__input-container">
                            <input className="auth__input" value={code}
                                   onChange={(e) => setCode(Number(e.target.value))}
                                   id="code" type="number" placeholder=" "/>
                            <label htmlFor="code" className="auth__input__label">Code</label>
                            <div id="code-error" className="auth__error-message hidden">
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
                                <span id="code-message"/>
                            </div>
                        </div>
                    )}
                </div>
                <button className="auth__button" type="button" onClick={(e) => handleNextClick(e)}>Next</button>
            </form>
        </div>
    );
};

export default RegisterEmailStep;