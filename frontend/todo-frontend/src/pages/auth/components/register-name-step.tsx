import {validateName, validateSurname} from "../../../utils/validators/validator.ts";
import React, {useState} from "react";
import {clearError, generateJwt, triggerShake} from "../../../utils/button-handlers/helpers.ts";
import {useAlerts} from "../../../hooks/alert/use-alerts.ts";
import {useNavigate} from "react-router-dom";
import {useLoading} from "../../../hooks/loading/use-loading.ts";

const RegisterNameStep = (props: {
    step: number,
    setStep: React.Dispatch<React.SetStateAction<number>>
}) => {
    const {step, setStep} = props;
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const {addAlert} = useAlerts();
    const navigate = useNavigate();
    const {startLoading, stopLoading} = useLoading();

    const handleNextClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();

        const nameMessage = validateName(name);
        const surnameMessage = validateSurname(surname);

        if (nameMessage.length > 0) {
            triggerShake('name', nameMessage);
        } else {
            clearError('name');
        }

        if (surnameMessage.length > 0) {
            triggerShake('surname', surnameMessage);
        } else {
            clearError('surname');
        }

        if (nameMessage.length === 0 && surnameMessage.length === 0) {
            const data = {
                name: name,
                surname: surname,
            }

            setStep(step + 1);
            generateJwt(step, data, addAlert, navigate, startLoading, stopLoading);
        }
    };

    return (
        <div className="auth-card__right">
            <h2 className="auth__title">Create Account</h2>
            <p className="auth__subtitle">Let's begin with your name. This helps us personalize your experience.</p>
            <form className="auth__form">
                <div className="auth__form-fields">
                    <div className="auth__input-container">
                        <input className="auth__input" id="name" value={name} onChange={(e) => setName(e.target.value)}
                               type="text" placeholder=" "/>
                        <label htmlFor="name" className="auth__input__label">Name</label>
                        <div id="name-error" className="auth__error-message hidden">
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
                            <span id="name-message"/>
                        </div>
                    </div>
                    <div className="auth__input-container">
                        <input className="auth__input" value={surname} onChange={(e) => setSurname(e.target.value)}
                               id="surname" type="text" placeholder=" "/>
                        <label htmlFor="surname" className="auth__input__label">Surname</label>
                        <div id="surname-error" className="auth__error-message hidden">
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
                            <span id="surname-message"/>
                        </div>
                    </div>
                </div>
                <button className="auth__button" type="button" onClick={(e) => handleNextClick(e)}>Next</button>
            </form>
            <div className="auth__footer">
                <span>Already have an account?</span>
                <a href="/login" className="auth__link">Log in</a>
            </div>
        </div>
    );
};

export default RegisterNameStep;