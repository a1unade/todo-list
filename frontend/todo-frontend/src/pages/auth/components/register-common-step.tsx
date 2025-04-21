import React, {useEffect, useRef, useState} from 'react';
import {validateBirthDate,} from "../../../utils/validators/validator.ts";
import {clearError, generateJwt, triggerShake} from "../../../utils/button-handlers/helpers.ts";
import errors from "../../../utils/validators/error-messages.ts";
import flatpickr from 'flatpickr';
import 'flatpickr/dist/flatpickr.min.css';
import {jwtDecode} from "jwt-decode";
import {RegistrationTokenData, RegistrationTokenPayload} from "../../../interfaces/jwt/registration-token-payload.ts";
import {useAlerts} from "../../../hooks/alert/use-alerts.ts";
import {useLoading} from "../../../hooks/loading/use-loading.ts";
import {useNavigate, useSearchParams} from "react-router-dom";

const RegisterCommonStep = (props: {
    step: number,
    setStep: React.Dispatch<React.SetStateAction<number>>,
}) => {
    const {step, setStep} = props;

    const [searchParams] = useSearchParams();
    const [birthdate, setBirthdate] = useState('');
    const [token, setToken] = useState<string>('');
    const [gender, setGender] = useState('');
    const {addAlert} = useAlerts();
    const {startLoading, stopLoading} = useLoading();
    const navigate = useNavigate();
    const dateInputRef = useRef<HTMLInputElement | null>(null);

    useEffect(() => {
        const token = searchParams.get('t');

        if (token) {
            setToken(token);
        }
    }, [searchParams]);

    useEffect(() => {
        if (dateInputRef.current) {
            const fp = flatpickr(dateInputRef.current, {
                dateFormat: "d-m-Y",
                onChange: (_, dateStr) => {
                    setBirthdate(dateStr);
                }
            });

            return () => {
                fp.destroy();
            };
        }
    }, []);

    const handleNextClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();

        const birthMessage = validateBirthDate(birthdate);

        if (birthMessage.length > 0) {
            triggerShake('birthdate', birthMessage);
        } else {
            clearError('birthdate');
        }

        if (gender.length === 0) {
            triggerShake('gender', errors.emptyGender);
        } else {
            clearError('gender');
        }

        if (birthMessage.length === 0 && gender.length > 0) {
            const jwtPayload = jwtDecode<RegistrationTokenPayload>(token);
            let parsedData : RegistrationTokenData | string = jwtPayload.Data;

            if (typeof parsedData === "string") {
                parsedData = JSON.parse(parsedData) as RegistrationTokenData;
            }

            const data = {
                name: parsedData.name,
                surname: parsedData.surname,
                birthDate: birthdate,
                gender: gender
            };

            setStep(step + 1);
            generateJwt(step, data, addAlert, navigate, startLoading, stopLoading);
        }
    };

    return (
        <div className="auth-card__right">
            <h2 className="auth__title">Create Account</h2>
            <p className="auth__subtitle">Your birthday and gender help us create a better experience.</p>
            <form className="auth__form">
                <div className="auth__form-fields">
                    <div className="auth__input-container">
                        <input
                            className="auth__input"
                            id="birthdate"
                            type="text"
                            placeholder=" "
                            ref={dateInputRef}
                            readOnly={true}
                            value={birthdate}
                            onChange={(e) => setBirthdate(e.target.value)}
                        />
                        <label htmlFor="birthdate" className="auth__input__label">Birthdate</label>
                        <div id="birthdate-error" className="auth__error-message hidden">
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
                                    <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z" />
                                </svg>
                            </span>
                            <span id="birthdate-message"/>
                        </div>
                    </div>
                    <div className="auth__input-container">
                        <select
                            className="auth__select"
                            id="gender"
                            value={gender}
                            onChange={(e) => setGender(e.target.value)}
                            required
                        >
                            <option value="" disabled hidden>Gender</option>
                            <option value="m">Male</option>
                            <option value="f">Female</option>
                        </select>
                        <label htmlFor="gender" className="auth__input__label">Gender</label>
                        <div id="gender-error" className="auth__error-message hidden">
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
                                    <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z" />
                                </svg>
                            </span>
                            <span id="gender-message"/>
                        </div>
                    </div>
                </div>
                <button className="auth__button" type="button" onClick={(e) => handleNextClick(e)}>Next</button>
            </form>
        </div>
    );
};

export default RegisterCommonStep;