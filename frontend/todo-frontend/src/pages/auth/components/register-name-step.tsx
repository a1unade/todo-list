

const RegisterNameStep = () => {
    return (
        <div className="auth-card__right">
            <h2 className="auth__title">Create Account</h2>
            <p className="auth__subtitle">Let's begin with your name. This helps us personalize your experience.</p>
            <form className="auth__form">
                <div className="auth__form-fields">
                    <input className="auth__input" type="text" placeholder="Name" />
                    <input className="auth__input" type="text" placeholder="Surname" />
                </div>
                <button className="auth__button">Next</button>
            </form>
            <div className="auth__footer">
                <span>Already have an account?</span>
                <a href="/login" className="auth__link">Log in</a>
            </div>
        </div>
    );
};

export default RegisterNameStep;