import React from "react";

const RegisterLastStep = (props: {
    step: number,
    setStep: React.Dispatch<React.SetStateAction<number>>
}) => {
    const {step, setStep} = props;

    const handleNextClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();

    }

    return (
        <div className="auth-card__right">
            <h2 className="auth__title">Create Account</h2>
            <p className="auth__subtitle">Registration complete. You’re ready to explore the platform.</p>
            <form className="auth__form">
                <button className="auth__button" type="button" onClick={(e) => handleNextClick(e)}>Next</button>
            </form>
        </div>
    );
};

export default RegisterLastStep;